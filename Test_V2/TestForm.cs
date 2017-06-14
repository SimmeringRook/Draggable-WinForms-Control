using System.Windows.Forms;

namespace Test_V2
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();

            CreateLabels(); //Generates labels to display the functionality
            SetUpContainers();
        }
        
        private void CreateLabels()
        {
            for (int i = 0; i < 4; i++)
            {
                Label label = new Label();

                if (i < 2)
                {
                    label.Text = "Draggable Label";
                    label.MouseDown += label_MouseDownEventHandler;
                }
                else
                {
                    label.Text = "Standard Label";
                }

                label.AutoSize = true;
                label.BorderStyle = BorderStyle.FixedSingle;

                flowLayoutPanel1.Controls.Add(label);
            }
        }

        /// <summary>
        /// Setting up the two flow layout panels that I want to be able
        /// to drag labels into
        /// 
        /// Allow the drop to occur:
        /// containerName.AllowDrop = true; 
        /// ----
        /// Subscribe to Event Handlers for Drag and Drop:
        /// containerName.DragEnter += container_DragEnterEventHandler;
        /// containerName.DragDrop += container_DragDropEventHandler;
        /// </summary>
        private void SetUpContainers()
        {
            flowLayoutPanel3.AllowDrop = true; 
            flowLayoutPanel3.DragEnter += container_DragEnterEventHandler;
            flowLayoutPanel3.DragDrop += container_DragDropEventHandler;

            flowLayoutPanel2.AllowDrop = true;
            flowLayoutPanel2.DragEnter += container_DragEnterEventHandler;
            flowLayoutPanel2.DragDrop += container_DragDropEventHandler;

            //This means you can freely drag the 2 draggable labels between
            //flow2 and flow 3 as much as you want
            //However, since flow1 doesn't have AllowDrop set to true, you
            //will not be able to drag the labels back into the original container
        }

        #region Draggable Control

        /* Really all that has to happen from the control that you want to drag is
         * to have them subscribe to this event handler;
         *          "label.MouseDown += label_MouseDownEvent;"
         */
        private void label_MouseDownEventHandler(object sender, MouseEventArgs e)
        {
            DoDragDrop(sender, DragDropEffects.Move);
        }


        /* If you wanted to do something fancy, like.. gray out the control that is
         * being dragged, we'd make an event handler here, and have the control also
         * subscribe to it upon creation.
         */
        #endregion

        #region Container Drag Drop Code

        /* This event handler checks to see if the container (that has the dragged 
         * control over it) allows Droping
         *  if true, 
         *      it will change the mouse cursor to inform the user they can drop
         *  else
         *      it will change the cursor to a "no access" cursor
         */
        private void container_DragEnterEventHandler(object sender, DragEventArgs e)
        {
            if ((e.AllowedEffect & DragDropEffects.Move) != 0)
                e.Effect = DragDropEffects.Move;
        }

        /* This event handler is the actual behaviour for what the container (that 
         * has the dragged control over it) needs to do when the mouse is released;
         * thereby finishing the drop.
         * 
         * In this case, we want to add the control to the container.
         */
        private void container_DragDropEventHandler(object sender, DragEventArgs e)
        {
            (sender as FlowLayoutPanel).Controls.Add(e.Data.GetData(typeof(Label)) as Label);
        }
        #endregion
    }
}
