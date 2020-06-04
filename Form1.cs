using System;
using System.Collections.Generic;
using System.Windows.Forms;


using RestSharp;
//using RestSharp.Authenticators;

namespace RestSharpSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void cmdExecute_Click(object sender, EventArgs e)
        {
            var url = txtUrl.Text;
            var client = new RestClient(url);
            //client.Authenticator = new HttpBasicAuthenticator("username", "password");
            var request = new RestRequest();
            request.Resource = "api/users";
            request.Method = Method.GET;
            /*
            var param = new MyClass { IntData = 1, StringData = "test123" };
            request.AddJsonBody(param);
            */
            var response = client.Get<NResult<List<Staff>>>(request);
            var json = response.Data;
            //Console.WriteLine(json.data);
            lstUsers.ValueMember = "staffId";
            lstUsers.DisplayMember = "staffName";
            lstUsers.DataSource = json.data;
        }
    }

    public class Staff
    {
        public string staffId { get; set; }
        public string staffName { get; set; }
    }

    public class NError
    {
        public bool hasError { get; set; }
        public int errNum { get; set; }
        public string errMsg { get; set; }
    }

    public class NResult<T>
        where T:class
    {
        public T data { get; set; }
        public NError errors { get; set; }
    }
}
