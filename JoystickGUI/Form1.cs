using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX.DirectInput;

namespace JoystickGUI
{
    public partial class Form1 : Form
    {
        System.Threading.Thread t;
        DirectInput directInput = new DirectInput();

        Guid joystickGUID = Guid.Empty;
        Joystick joystick;
        List<DeviceInstance> devicesList = new List<DeviceInstance>();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            deviceInstanceList.Items.Clear();
            foreach (var deviceInstance in directInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
            {
                devicesList.Add(deviceInstance);
                deviceInstanceList.Items.Add(deviceInstance.InstanceName);
            }

            foreach (var deviceInstance in directInput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
            {
                devicesList.Add(deviceInstance);
                deviceInstanceList.Items.Add(deviceInstance.InstanceName);
            }
        }

        public void klasa()
        {
            while (true)
            {
                joystick.Poll();

                var data = joystick.GetBufferedData();

                foreach (var state in data)
                {
                    if (state.Offset == JoystickOffset.X)
                    {
                        SetText("" + state.Value, textBoxX);
                    }
                    if (state.Offset == JoystickOffset.Y)
                    {
                        SetText("" + state.Value, textBoxY);
                    }
                    if (state.Offset == JoystickOffset.Z)
                    {
                        SetText("" + state.Value, textBoxZ);
                    }
                    if (state.Offset == JoystickOffset.Buttons0)
                    {
                        checkBox1.Checked = true;
                        if (state.Value.Equals(0)) checkBox1.Checked = false;
                    }
                    if (state.Offset == JoystickOffset.Buttons1)
                    {
                        checkBox2.Checked = true;
                        if (state.Value.Equals(0)) checkBox2.Checked = false;
                    }
                    if (state.Offset == JoystickOffset.Buttons2)
                    {
                        checkBox3.Checked = true;
                        if (state.Value.Equals(0)) checkBox3.Checked = false;
                    }
                    if (state.Offset == JoystickOffset.Buttons3)
                    {
                        checkBox4.Checked = true;
                        if (state.Value.Equals(0)) checkBox4.Checked = false;
                    }
                    if (state.Offset == JoystickOffset.Buttons4)
                    {
                        checkBox5.Checked = true;
                        if (state.Value.Equals(0)) checkBox5.Checked = false;
                    }
                    if (state.Offset == JoystickOffset.Buttons5)
                    {
                        checkBox6.Checked = true;
                        if (state.Value.Equals(0)) checkBox6.Checked = false;
                    }
                    if (state.Offset == JoystickOffset.Buttons6)
                    {
                        checkBox7.Checked = true;
                        if (state.Value.Equals(0)) checkBox7.Checked = false;
                    }
                    if (state.Offset == JoystickOffset.Buttons7)
                    {
                        checkBox8.Checked = true;
                        if (state.Value.Equals(0)) checkBox8.Checked = false;
                    }
                    if (state.Offset == JoystickOffset.Buttons8)
                    {
                        checkBox9.Checked = true;
                        if (state.Value.Equals(0)) checkBox9.Checked = false;
                    }
                    if (state.Offset == JoystickOffset.Buttons9)
                    {
                        checkBox10.Checked = true;
                        if (state.Value.Equals(0)) checkBox10.Checked = false;
                    }
                    if (state.Offset == JoystickOffset.Buttons10)
                    {
                        checkBox11.Checked = true;
                        if (state.Value.Equals(0)) checkBox11.Checked = false;
                    }

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            joystickGUID = devicesList[deviceInstanceList.SelectedIndex].InstanceGuid;
            joystick = new Joystick(directInput, joystickGUID);
            joystick.Properties.BufferSize = 128;

            joystick.Acquire();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            t = new System.Threading.Thread(klasa);
            t.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            t.Suspend();
        }

        delegate void SetTextCallback(string text, TextBox textBox);

        private void SetText(string text, TextBox textBox)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (textBox.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text, textBox });
            }
            else
            {
                textBox.Text = text;
            }
        }
    }
}
