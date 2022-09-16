var loadData = function () {
    $.ajax({
        type: "GET",
        url: "/Home/GetAllData",
        success: onSuccess,
        failure: function (response) {

        },
        error: function (response) {

        },
    });
};
var onSuccess = function (response) {

    $('#tblclient').DataTable({
        bLengthChange: true,
        lengthMenu: [[5, 20, -1], [5, 10, "All"]],
        bFilter: true,
        bPaginate: true,
        data: response,
        destroy: true,
        columns: [{ 'data': 'UserID' },
            { 'data': 'UserName' },
            { 'data': 'Salary' },
            { 'data': 'Email', "class": "edit3" },
            { 'data': 'CityName' },
            { 'data': 'StateName' },
            { 'data': 'Gender' },
            {
                'data': 'UserID', "width": "150px", "render": function (UserID) {
                    return '<button type="button" id="Updatecrd" data-toggle="modal" data-target="#staticBackdrop" class="btn btn-primary Updatecrd" onclick="return GetbyID(' + UserID + ')"><i class="fa fa-edit">&nbspUpdate</i></button>'
                }
            },
            {
                'data': 'UserID', "width": "150px", "render": function (UserID) {
                    return '<button type="button" id="deleteemp" class="btn btn-danger" onclick="Delete(' + UserID + ')"><i class="fa fa-trash">&nbspDelete</i></button>'
                }
            },

        ]
    })
}
loadData();

var state = function (stateid) {
    $.ajax({
        type: 'GET',
        url: '/Home/GetState',
        success: function (response) {
            var droplist = "<option value='0'>--Select State--</option>";
            for (var i = 0; i < response.length; i++) {
                droplist += "<option value='" + response[i].StateID + "'>" + response[i].StateName + "</option>";
            }
            $('#ddState').html(droplist);
            $('#ddState').val(stateid);
        },
        error: function (response) {

        }
    });
};
state();

var city = function (cityid) {
    $.ajax({
        type: 'GET',
        url: '/Home/GetCity?id=' + $('#ddState').val(),
        success: function (response) {
            var droplist = "<option value='0'>--Select City--</option>";
            for (var i = 0; i < response.length; i++) {
                droplist += "<option value='" + response[i].CityID + "'>" + response[i].CityName + "</option>";
            }
            $('#ddCity').html(droplist);
            $('#ddCity').val(cityid);
        },
        error: function (response) {

        }
    });
}

var gender = function (cityid) {
    var gen = "<option value='0' selected>--Select Gender--</option>";
    gen += "<option value='Male'>Male</option>";
    gen += "<option value='Female'>Female</option>";
    gen += "<option value='Others'>Others</option>";
    $('#ddGender').html(gen);
}
gender();
$('#ddState').change(function () {
    city();
});

var GetbyID = function (UserID) {
    $.ajax({
        type: "GET",
        url: "/Home/GetSpecififcData?id=" + UserID,
        success: function (response) {
            $('#txtUserID').val(response[0].UserID);
            $('#txtUserName').val(response[0].UserName);
            $('#txtPassCode').val(response[0].PassCode);
            $('#txtSalary').val(response[0].Salary);
            $('#txtEmail').val(response[0].Email);
            $('#ddState').val(response[0].StateID);
            city(response[0].CityID);
            $('#ddGender').val(response[0].Gender);
        },
        error: function (response) {

        },
    });
}
$('.clr').click(function () {
    $('#txtUserID').val('');
    $('#txtUserName').val('');
    $('#txtPassCode').val('');
    $('#txtSalary').val('');
    $('#txtEmail').val('');
    $('#ddGender').val('');
    $('#ddState').val(0);
    $('#ddCity').val(0);
    gender();
    return true;
});


var updateonsubmit = function () {
    var UserID = document.getElementById('txtUserID').value;
    var UserName = document.getElementById('txtUserName').value;
    var PassCode = document.getElementById('txtPassCode').value;
    var Salary = document.getElementById('txtSalary').value;
    var Email = document.getElementById('txtEmail').value;
    var ddState = document.getElementById("ddState").value;
    var ddCity = document.getElementById("ddCity").value;
    var f = document.getElementById("ddGender");
    var ddGender = f.options[f.selectedIndex].text;
    $.ajax({
        type: "GET",
        url: "/Home/updateData",
        data: { UserID: UserID, UserName: UserName, PassCode: PassCode, Salary: Salary, ddstate: ddState, ddcity: ddCity, Email: Email, ddGender: ddGender },
        //success: onSuccess,
        success: function (response) {
            alert(response);
            location.reload();
        },
        error: function (response) {

        },
    });
}

var Delete = function (UserID) {
    var conf = confirm("Are You Sure.. You Want to delete the Record");
    if (conf) {
        $.ajax({
            type: 'GET',
            url: '/Home/DeleteData',
            data: { EID: UserID },
            contentType: 'application/json',
            dataType: 'json',
            success: function (response) {
                alert(response);
                location.reload();
            },
            error: function (response) {

            }

        })
    };
}