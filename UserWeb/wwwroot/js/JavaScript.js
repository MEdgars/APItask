// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
var HttpClient = function () {
    this.get = function (aUrl, aCallback) {
        var anHttpRequest = new XMLHttpRequest();
        anHttpRequest.onreadystatechange = function () {
            if (anHttpRequest.readyState == 4 && anHttpRequest.status == 200)
                aCallback(anHttpRequest.responseText);
        }
        anHttpRequest.open("GET", aUrl, true);
        anHttpRequest.send(null);
    }
}
var theurl = 'https://usersfunc.azurewebsites.net/api/users';
var client = new HttpClient();
client.get(theurl, function (response) {
    var response1 = JSON.parse(response);
    // alert(response);
    var table = '<tr><th>Name</th><th>Surname</th><th>ID</th><th>Manager ID</th><th>Position</th><th>Hire date</th><th>Discharge Date</th></tr>';
    for (r = 0; r < 1; r++) {

        for (var c = 0; c < response1.length; c++) {
            table += '<tr>';
            table += '<td>' + response1[c].firstName + '</td>';
            table += '<td>' + response1[c].lastName + '</td>';
            table += '<td><a href="https://usersfunc.azurewebsites.net/api/users/' + response1[c].id + '">' + response1[c].id + '</a></td>';
            table += '<td>' + response1[c].managerId + '</td>';
            table += '<td>' + response1[c].position + '</td>';
            table += '<td>' + response1[c].hireDate + '</td>';
            table += '<td>' + response1[c].dischargeDate + '</td>';
            table += '</tr>'
        }

    }
    document.write('<table style="width:100% display: table; border-collapse: separate; border-spacing: 2px; border-color: gray;">' + table + '</table>')
});