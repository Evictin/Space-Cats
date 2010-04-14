using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.Text;
using System.IO;

namespace Space_Cats_V1._2
{
    class MissionManager
    {
        private static List<IArtificialIntelligence> z_AIList;
        private List<MissionScriptNode> z_script;
        private List<MissionScriptNode> z_actives;
        private static MissionManager z_instance=null;
        private int z_loadedMission;
        private int z_missionTime;
        private int z_nextCommand;
        public int MissionTime
        { get { return z_missionTime; } }
        public int NextCommand
        {
            get { return z_nextCommand; }
            set { z_nextCommand = value; }
        }
        private ContentManager z_content;

        public static MissionManager Instance
        { get { return z_instance; } }


        public MissionManager(ContentManager content)
        {
            z_AIList = new List<IArtificialIntelligence>();
            z_script = new List<MissionScriptNode>();
            z_actives = new List<MissionScriptNode>();
            z_loadedMission = 0;
            z_missionTime = 0;
            z_instance = this;
            z_nextCommand = 0;
            z_content = content;
        }

        public static MissionManager GetInstance()
        {
            return z_instance;
        }

        public void LoadMission(int mission)
        {
            BinaryReader br;
            string input;
            int fileID;
            Rectangle fileViewport = new Rectangle(0, 0, 0, 0);

            if (z_loadedMission != mission)
            {
                // Clear the AI list
                z_AIList.Clear();

                // clear the Mission Script List
                z_script.Clear();
                z_actives.Clear();

                br = new BinaryReader(File.OpenRead(z_content.RootDirectory + "\\AI\\Mission 3.msn"));
                try
                {
                    fileID = br.ReadInt32();
                    if (fileID == 12)
                    {
                        fileViewport.Width = br.ReadInt32();
                        fileViewport.Height = br.ReadInt32();
                        do
                        {
                            input = br.ReadString();
                            if (input.CompareTo("AI_SCRIPT") == 0)
                            {
                                z_AIList.Add(new AI_Script(fileViewport, br));
                            }
                            else if (input.CompareTo("MISSION_SCRIPT") == 0)
                            {
                                LoadMissionScriptFromFile(br);
                            }
                        } while (input.CompareTo("EOF") != 0);
                    }
                }
                finally
                {
                    br.Close();
                }

                // Read in the mission script
                z_loadedMission = mission;
            }
            z_missionTime = 0;
        }

        public void LoadMissionScriptFromFile(BinaryReader br)
        {
            MissionScriptNode node;
            do
            {
                node = MissionScriptNode.readNodeFromFile(br);
                this.z_script.Add(node);
            } while (node.Command != MissionScriptNode.CommandID.End);
        }

        public static IArtificialIntelligence GetAI(int id)
        {
            foreach (IArtificialIntelligence ai in z_AIList)
            {
                if (ai.ID == id)
                    return ai;
            }
            return null;
        }

        public void update(GameTime gameTime)
        {
            z_missionTime += gameTime.ElapsedGameTime.Milliseconds;
            // for each command in the script list that occurs at the current timestamp....
            while ((NextCommand<z_script.Count)&&(z_script[NextCommand].CanExecute(z_missionTime)))
            {
                // add it to the list of active commands
                z_actives.Add(z_script[NextCommand]);
                NextCommand++;
            }
            // execute each active command
            for (int i = 0; i < z_actives.Count; i++)
            {
                    z_actives[i].Execute(gameTime);
                // if the command has done everything it needs to do then remove it from the active list
                if (z_actives[i].IsDone)
                    z_actives.RemoveAt(i--);
            }
        }

        public void EndMission()
        {
            Console.WriteLine("End of Mission.");
        }

        public void reset()
        {
            z_missionTime = 0;
            foreach (MissionScriptNode node in z_script)
                node.reset();
            z_actives.Clear();
            NextCommand = 0;
        }

       

    }
}
