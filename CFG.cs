using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LabMid
{
    public partial class CFG : Form
    {
        public CFG()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Initial_State = "Idle";
            string Error_State = "Error";
            string Unrecognized_Command_Error = "Unrecognized";

            // Define dictionary for each valid command transition
            var dict = new Dictionary<string, Dictionary<string, string>>
            {
                { "Idle", new Dictionary<string, string>
                    {
                        { "start", "Moving" },       // Car can only start from idle
                        { "stop", "Idle" },          // Stop keeps it idle
                        { "accelerate", Error_State }, // Can't accelerate if idle
                        { "right", Error_State },      // Can't turn if idle
                        { "left", Error_State }       // Unsupported command
                    }
                },
                { "Moving", new Dictionary<string, string>
                    {
                        { "stop", "Idle" },          // Can stop to go idle
                        { "accelerate", "Moving" },  // Can accelerate while moving
                        { "right", "TurningRight" }, // Can turn right while moving
                        { "left", Error_State }       // Unsupported command
                    }
                },
                { "TurningRight", new Dictionary<string, string>
                    {
                        { "stop", "Idle" },          // Stop brings it back to idle
                        { "start", "Moving" },       // Can start moving again
                        { "accelerate", "Moving" },  // Can accelerate from turning
                        { "right", "TurningRight" }, // Can continue turning right
                        { "left", Error_State }       // Unsupported command
                    }
                }
            };

            // Define a set of all valid commands
            var validCommands = new HashSet<string> { "start", "stop", "accelerate", "right", "left" };

            // Capture and split input command sequence
            string[] commands = textBox1.Text.ToLower().Split(' ');
            string state = Initial_State;

            // Process each command in sequence
            foreach (string command in commands)
            {
                // Check if command is valid; if not, set state to Unrecognized_Command_Error
                if (!validCommands.Contains(command))
                {
                    state = Unrecognized_Command_Error;
                    break;
                }

                // Check for valid transitions
                if (dict[state].ContainsKey(command))
                {
                    // Transition to next state based on command
                    state = dict[state][command];
                }
                else
                {
                    // If command not allowed in the current state, mark as error
                    state = Error_State;
                    break;
                }
            }

            // Display results based on final state
            if (state == Error_State)
            {
                label1.Text = "ERROR: Command not valid in the current state.";
            }
            else if (state == Unrecognized_Command_Error)
            {
                label1.Text = "ERROR: Unrecognized command.";
            }
            else
            {
                label1.Text = "RESULT OKAY";
            }

            // Clear the input field after processing
           textBox1.Text = string.Empty;
        }
    }


}


