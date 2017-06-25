
function formatoaceptacion(lunch, prop) {
    var color = 'maroon';
    var estado = lunch[prop];
    if (estado == 'Orden Compra') color = 'green';
    if (estado == 'Rechazado') color = 'red';
    if (estado == 'Pendiente') color = 'maroon';
    if (estado == 'Inventario') color = 'blue';
    return "<div style='color:" + color + ";text-width:bold;'>" + estado + " </div>";
}
$(document).ready(function () {


            
            $("#btnExport").click(function () {
                tableToExcel('assets-data-table', 'Table');
            });
          

            var tableToExcel = (function () {
                var uri = 'data:application/vnd.ms-excel;base64,'
                    , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>'
                    , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
                    , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
                return function (table, name) {
                    if (!table.nodeType) table = document.getElementById(table)
                    var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
                    window.location.href = uri + base64(format(template, ctx))
                }
            })()
            $('#assets-data-table').DataTable({
                dom: 'Bfrtip',
                buttons: [
                    {
                        extend: 'copyHtml5',
                        exportOptions: {
                            columns: ':contains("Office")'
                        }
                    },
                    'excelHtml5',
                    'csvHtml5',
                    'pdfHtml5'
                ],
                "scrollY": true,
                "scrollX": true,
                paging: false,
                datawidth: '*',
                "language": {
                    "url": "https://cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
                }
            });
        });
    