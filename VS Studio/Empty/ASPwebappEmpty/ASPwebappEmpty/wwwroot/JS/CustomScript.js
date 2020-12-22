function UserDeleteConfirmation(_userid, _username) {
    if (confirm("Delete the user:" + _username + "")) {
        $.post('DeleteUser',
            { userid: "" + _userid + "" }, function (data) {
                //alert("Success " + data.success);
                console.log(data);
                window.location.href = window.location.href;
        });
    }
}
function RoleDeleteConfirmation(_roleid, _rolename) {
    if (confirm("Delete the role:" + _rolename + "")) {
        var call = $.post('DeleteRole',
            { roleid: "" + _roleid + "" }, function (data) {
                //alert("Success " + data.success);
                console.log(data);
                window.location.href = window.location.href;
        }).done(function (data) {
            console.log(data);
            console.log(_rolename + " deleted successfully.");
            })
            .fail(function (_data) {
                console.log(_data.responseText);
                window.location.href = "PageNotFound?data=" + _data.statusText;
                //$.get('PageNotFound', { data: _data.statusText }, function (_data2) {
                //    console.log(_data2);
                //}).done(function (data) {
                //    console.log(data);
                //}).fail(function (data) {
                //    console.log(data);
                //    alert('Contact Administrator');
                //});
            });
    }
}