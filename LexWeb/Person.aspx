<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Person.aspx.cs" Inherits="LexWeb.Person" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
        <tr><td>Namn</td><td><%= MyPersonInfo.Name %></td></tr>
        <tr><td>Adress</td><td><%= MyPersonInfo.Adress %></td></tr>
        <tr><td>Stad</td><td><%= MyPersonInfo.City %></td></tr>
        <tr><td>Telefon</td><td><%= MyPersonInfo.TelephoneAsString()%></td></tr>
        <tr><td>Personnummer</td><td><%= MyPersonInfo.Ssn %></td></tr>
    </table>
    </div>
    </form>
</body>
</html>
