
$(document).ready(function () {
    $('#searchbox').keyup(function (event) {
        // enter has keyCode = 13, change it if you want to use another button
        if (event.keyCode == 13) {
            this.form.submit();
            return false;
        }
    });
});
$("#post").keyup(function (event) {
    if (event.keyCode == 13) {
        debugger;
        $("#myBtn").click();
    }
});
$('#file-input').change(function (e) {
    debugger;
    var fileName = e.target.files[0].name;
    let photo = document.getElementById("photo-path").innerHTML = fileName;
});

function addPhoto() {
    debugger;
    let formData = new FormData();
    let totalFiles = document.getElementById("file-input").files.length;
    for (var i = 0; i < totalFiles; i++) {
        let file = document.getElementById("file-input").files[i];
        formData.append("file-input", file);
    }

    $.ajax({
        type: "POST",
        url: '/Account/UploadPostImage',
        data: formData,
        dataType: 'json',
        contentType: false,
        processData: false,
        success: function (response) {
            alert('succes!!');
        }
    })
}

function postTweet(userid) {
    let text = $("#post").val();
    let photo = document.getElementById("photo-path").innerHTML;
    debugger;
    let posttweet = {
        UserId: userid,
        Description: text,
        Photo: photo
    };
    debugger;
    $.ajax({
        type: "POST",
        url: "/Home/PushPost",
        data: posttweet,
        //dataType :"Json",
        success: function (response) {
            if (response == 1) {
                $("#photo-path").html("");
                refleshpostlist();
                getirici();
            }
            else {

            };
        },
        error: function () {

        }
    });
}

function getirici() {
    debugger;
    $.ajax({
        type: "POST",
        url: "/Home/RefleshCreatePost",
        //data: { id: id },
        dataType: "html",
        success: function (data) {
            $('#panel').html(data);
        },

    });
}
function refleshpostlist() {
    debugger;
    $.ajax({
        type: "POST",
        url: "/Home/RefleshPostList",
        //data: { id: id },
        dataType: "html",
        success: function (data) {
            $('#panel2').html(data);
        },

    });
}
