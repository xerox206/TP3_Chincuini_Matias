using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP3_Chincuini_Matias
{
    public partial class RegistrosContables : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                completarTabla();
            }
        }

        private void completarTabla()
        {
            try
            {
                DataView dv = (DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);


                if (dv != null && dv.Count > 0)
                {
                    // Rellenar cabecera
                    TableRow headerRow = new TableRow();

                    TableCell headerCell1 = new TableCell();
                    headerCell1.Text = "ID";
                    headerRow.Cells.Add(headerCell1);

                    TableCell headerCell2 = new TableCell();
                    headerCell2.Text = "IDCuenta";
                    headerRow.Cells.Add(headerCell2);

                    TableCell headerCell3 = new TableCell();
                    headerCell3.Text = "Descripcion";
                    headerRow.Cells.Add(headerCell3);

                    TableCell headerCell4 = new TableCell();
                    headerCell4.Text = "Tipo";
                    headerRow.Cells.Add(headerCell4);


                    TableCell headerCell5 = new TableCell();
                    headerCell5.Text = "Precio";
                    headerRow.Cells.Add(headerCell5);

                    Table1.Rows.Add(headerRow);

                    // Rellenar las filas
                    foreach (DataRowView rowView in dv)
                    {
                        DataRow row = rowView.Row;
                        TableRow tableRow = new TableRow();

                        TableCell cell1 = new TableCell();
                        cell1.Text = row["id"].ToString();
                        tableRow.Cells.Add(cell1);

                        TableCell cell2 = new TableCell();
                        cell2.Text = row["idCuenta"].ToString();
                        tableRow.Cells.Add(cell2);

                        TableCell cell3 = new TableCell();
                        cell3.Text = row["descripcion"].ToString();
                        tableRow.Cells.Add(cell3);

                        TableCell cell4 = new TableCell();
                        cell4.Text = row["tipo"].ToString();
                        if (cell4.Text == "False")
                        {
                            cell4.Text = "Debe";
                        }
                        else
                        {
                            cell4.Text = "Haber";
                        }

                        tableRow.Cells.Add(cell4);

                        TableCell cell5 = new TableCell();
                        cell5.Text = row["monto"].ToString();
                        tableRow.Cells.Add(cell5);

                        Table1.Rows.Add(tableRow);
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "AlertScript", $"alert('Error');", true);
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlDataSource2.InsertParameters["monto"].DefaultValue = TextBox1.Text;
            SqlDataSource2.InsertParameters["idCuenta"].DefaultValue = DropDownList1.SelectedValue;
            SqlDataSource2.InsertParameters["tipo"].DefaultValue = (DropDownList2.SelectedValue == "debe") ? "False" : "True";

            try
            {
                int result = SqlDataSource2.Insert();
                if (result != 0)
                {
                    Label1.Text = "Se agrego " + result.ToString();
                    completarTabla();
                    TextBox1.Text = "";
                }
                else
                {
                    Label1.Text = "No se agrego";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string selectedValue = DropDownList3.SelectedValue;

            // Asegúrate de que selectedValue no sea null antes de asignarlo
            if (!string.IsNullOrEmpty(selectedValue))
            {
                SqlDataSource2.UpdateParameters["id"].DefaultValue = selectedValue;
                SqlDataSource2.UpdateParameters["monto"].DefaultValue = TextBox1.Text;
                SqlDataSource2.UpdateParameters["idCuenta"].DefaultValue = DropDownList1.SelectedValue;
                SqlDataSource2.UpdateParameters["tipo"].DefaultValue = (DropDownList2.SelectedValue == "debe") ? "False" : "True";
                int result = SqlDataSource2.Update();

                if (result != 0)
                {
                    Label1.Text = "Se modifico " + result.ToString();
                    completarTabla();
                    TextBox1.Text = "";
                }
                else
                {
                    Label1.Text = "No se modifico";

                }
            }
            else
            {
                Label1.Text = "Seleccione un valor válido antes de intentar modificar.";

            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            int result = SqlDataSource2.Delete();
            if (result != 0)
            {
                Label1.Text = "Se elimino " + result.ToString();
                completarTabla();
                TextBox1.Text = "";
            }
            else
            {
                Label1.Text = "No se elimino";

            }
        }
        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dv = (DataView)SqlDataSource3.Select(DataSourceSelectArguments.Empty);
            if (dv != null && dv.Count > 0)
            {
                DataRowView row = dv[0];
                TextBox1.Text = row["monto"].ToString();
                DropDownList1.SelectedValue = row["idCuenta"].ToString();
                DropDownList2.SelectedValue = row["tipo"].ToString();
            }
            completarTabla();
        }

    }
}
        