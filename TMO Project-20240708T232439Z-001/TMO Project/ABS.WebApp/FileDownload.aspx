<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="FileDownload.aspx.cs" Inherits="ABS.WebApp.FileDownload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="DownloadButton" CssClass="btn btn-default" runat="server" Text="Download" OnClick="DownloadButton_Click" />
            <asp:Button runat="server" Text="DownLoadFromClient" ID="btnDownloadFromClient" >

            </asp:Button><asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>

        </div>
    </form>
</body>
</html>
<script>
    $(document).ready(function () {
        $('#btnDownloadFromClient').on('click', function () {
            var url = 'https://localhost:7170/api/ExcelGeneration/client/download';
            const data = {FileName: "DataGenerator.xls", SheetName: "sheet1" }

            const $button = $(this);
            $button.prop("disabled", true);

            $.ajax({
                url: url,
                type: 'POST',                
                contentType: 'application/json',
                data: JSON.stringify(data),            
                xhrFields: {
                    responseType: 'blob',
                },
                success: function (blob, status, xhr) {  
                    
                    var blob = new Blob([data], {
                        type:
                            'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
                    });

                    var downloadUrl = URL.createObjectURL(blob);
                    var fileName = data.FileName; 
                    
                    var link = document.createElement('a');
                    link.href = downloadUrl;
                    link.download = fileName;                    
                    document.body.appendChild(link);
                    
                    link.click();
                    
                    document.body.removeChild(link);
                    URL.revokeObjectURL(downloadUrl);
                },
                error: function (xhr, status, error) {
                    console.error("Error occurred: ", error);
                },
                complete: function () {
                    $button.prop('disabled', false);
                }
            });
            return false;
        })
    })
</script>
