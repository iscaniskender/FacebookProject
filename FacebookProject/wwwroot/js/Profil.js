//$(document).ready(function () {
//    debugger;
//   let id= $("#userid").val();
//    $.ajax({
//        type: "POST",
//        url:"/Profil/partialpost",
//        data: { id: id },
//        success: success,
//        dataType: dataType
//    });
//});





function AddFriend(friendname) {
    debugger;
    $.ajax({
        type: "POST",
        url: "/Profil/AddFriend",
        data: { friend: friendname},
        //dataType :"Json",
        success: function (response) {
            if (response == 1) {
                $("#addfirend").html("Pendig friend Request");
            }
        },
       
    });
}
function KabulET(userid) {
    debugger;
    $.ajax({
        type: "POST",
        url: "/Profil/AcceptFriends",
        data: { userid: userid },
        //dataType :"Json",
        success: function (response) {
            $("th[id=th-" + userid + "]").remove();
        },

    });
}
function Reddet(userid) {
   
    debugger;
    $.ajax({
        type: "POST",
        url: "/Profil/deleteFriends",
        data: { userid: userid },
        //dataType :"Json",
        success: function (response) {
            $("th[id=th-" + userid + "]").remove();
        },

    });
}