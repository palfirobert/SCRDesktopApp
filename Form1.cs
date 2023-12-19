using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCRDesktopApp
{
    public partial class Form1 : Form
    {
        EmployeeService employeeService;
        List<Employee> employeeList;
        List<Department> departmentList;
        int departmentIdForEmployee;
        int departmentIdForManager;
        public Form1()
        {
            InitializeComponent();
            employeeService = new EmployeeService();
            employeeService.createConnection();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var token = employeeService.login();
            this.employeeList = employeeService.GetEmployees();
            this.departmentList = employeeService.getDepartments();

            comboBox1.DataSource = departmentList;
            comboBox1.DisplayMember = "Description";
            comboBox1.ValueMember = "Id";

            comboBox2.DataSource = departmentList;
            comboBox2.DisplayMember = "Description";
            comboBox2.ValueMember = "Id";

            listBox3.DataSource = this.employeeService.GetEmployees();
            listBox3.DisplayMember = "Name";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.DataSource = this.employeeService.getEmployeesPerDepartment(this.departmentIdForEmployee);
            listBox1.DisplayMember = "Name";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem is int selectedValue)
            {
                // Cast the selected item to the Department type
                this.departmentIdForEmployee = (int)comboBox1.SelectedValue;
                Console.WriteLine(comboBox1.SelectedValue);
                // Use the departmentId as needed
                Console.WriteLine("Selected Department ID: " + this.departmentIdForEmployee);
            }
            else
            {
                Department selectedDepartment = (Department)comboBox1.SelectedItem;
                this.departmentIdForEmployee = selectedDepartment.Id;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox2.DataSource = this.employeeService.getManagersPerDepartment(this.departmentIdForManager);
            listBox2.DisplayMember = "Name";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null && comboBox2.SelectedItem is int selectedValue)
            {
                // Cast the selected item to the Department type
                this.departmentIdForManager = (int)comboBox2.SelectedValue;
                Console.WriteLine(comboBox2.SelectedValue);
                // Use the departmentId as needed
                Console.WriteLine("Selected Department ID: " + this.departmentIdForManager);
            }
            else
            {
                Department selectedDepartment = (Department)comboBox2.SelectedItem;
                this.departmentIdForManager = selectedDepartment.Id;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (var selectedItem in listBox3.SelectedItems)
            {
                if (selectedItem is Employee employee)
                {
                    // Get the email address from the Employee object
                    string emailAddress = employee.Email;
                    Console.WriteLine(emailAddress);
                    // Use your SMTP server credentials
                    string smtpServer = "smtp.gmail.com";
                    int smtpPort = 587; // or the appropriate port
                    string smtpUsername = "palfi.robert1357@gmail.com";
                    string smtpPassword = "puzi ephu mmrv jgru";

                    // Set up the SmtpClient
                    using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                    {
                        smtpClient.EnableSsl = true;
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);

                        // Create a MailMessage
                        MailMessage mailMessage = new MailMessage();
                        mailMessage.From = new MailAddress(smtpUsername);
                        mailMessage.To.Add(emailAddress);
                        mailMessage.Subject = "Work info";
                        mailMessage.Body = textBox1.Text;

                        // Send the email
                        try
                        {
                            smtpClient.Send(mailMessage);
                            Console.WriteLine($"Email sent to {emailAddress}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Failed to send email to {emailAddress}. Error: {ex.Message}");
                            // Handle the exception as needed
                        }
                    }
                }
            }
            textBox1.Text = "";
        }
    }
}
