
function isPasswordMatch() {
    var password = $("#Password").val();
    var confirmPassword = $("#RePassword").val();

    if (password != confirmPassword) $("#divCheckPassword").html("Passwords do not match!");
    else $("#divCheckPassword").html("Passwords match.");
}

$(document).ready(function () {
    $("#RePassword").keyup(isPasswordMatch);
});

function control() {
    debugger;
    let FullName = $("#FullName").val();
    let Username = $("#Username").val();
    let Password = $("#Password").val();
    let Repassword = $("#RePassword").val();
    let durum = 0;
    if (FullName == null || FullName == "") {
        alertMessages("Kullanıcı adı boş bırakılamaz");
        return false;
    }
    if (Password == null || Password == "" || RePassword == null || Repassword == "") {
        alertMessages("Şifre boş bırakılamaz")
        return false;
    }
    if (!(Password == Repassword)) {
        alertMessages("Şifreler uyuşmuyor")
        return false;
    }
    if (Username == null || Username == "") {
        alertMessages("Email boş bırakılamaz")
        return false;
    }
    register();
}


$('#profil').change(function (e) {
    var fileName = e.target.files[0].name;
    document.getElementById("photo-path").innerHTML = fileName;
});
$('#background').change(function (e) {
    var fileName = e.target.files[0].name;
    document.getElementById("photo-path2").innerHTML = fileName;
});

function addPhoto() {
    let formData = new FormData();
    let totalFiles = document.getElementById("profil").files.length;
    for (var i = 0; i < totalFiles; i++) {
        let file = document.getElementById("profil").files[i];
        formData.append("profil", file);
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
function addBack() {
    let formData = new FormData();
    let totalFiles = document.getElementById("background").files.length;
    for (var i = 0; i < totalFiles; i++) {
        let file = document.getElementById("background").files[i];
        formData.append("background", file);
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


function register() {
    debugger;
    let FullName = $("#FullName").val();
    let Username = $("#Username").val();
    let Password = $("#Password").val();
    let PhoneNumber = $("#PhoneNumber").val();
    let LiveCity = $("#LiveCity").val();
    let HomeLand = $("#HomeLand").val();
    let ProfilPhoto = document.getElementById("photo-path").innerHTML;
    let BackgroundImage = document.getElementById("photo-path2").innerHTML;

    let Register = {
        FullName: FullName,
        Username: Username,
        Password: Password,
        PhoneNumber: PhoneNumber,
        LiveCity: LiveCity,
        HomeLand: HomeLand,
        Isactive: true,
        Isdeleted: false,
        ProfilPhoto: ProfilPhoto,
        BackgroundImage: BackgroundImage
    };
    debugger;
    $.ajax({
        type: "POST",
        url: "/Account/Register",
        data: Register,
        //dataType :"Json",
        success: function (response) {
            if (response == 1) {

                successMessages("Kayıt Başarılı");
                FullName = $("#FullName").html("");
                Username = $("#Username").html("");
                Password = $("#Password").html("");
                PhoneNumber = $("#PhoneNumber").html("");
                LiveCity = $("#LiveCity").html("");
                HomeLand = $("#HomeLand").html("");
            }
            else {

                alertMessages("Kayıt Sırasında hata oluştu");
            }

        },
        error: function () {

        }
    });
}

function login() {
    debugger;
    let UsernameLogin = $("#UsernameLogin").val();
    let PasswordLogin = $("#PasswordLogin").val();
    let Login = {
        Password: PasswordLogin,
        Username: UsernameLogin
    };
    debugger;
    $.ajax({
        type: "POST",
        url: "/Account/Login",
        data: Login,
        //dataType :"Json",
        success: function (response) {
            if (response == 1) {
                location.href = "/Home/Index";
            }
            else {

                alertMessages("Giriş sırasında hata oluştu");
            }
        },
        error: function () {

        }
    });
}

function alertMessages(message) {
    const cardbody = document.querySelector("#alert");

    const div = document.createElement("div");
    div.className = `alert alert-danger`;
    div.textContent = message;

    cardbody.append(div);

    setTimeout(function () {
        div.remove();
    }, 2500);
}

function successMessages(message) {
    const cardbody = document.querySelector("#alert");

    const div = document.createElement("div");
    div.className = `alert alert-success`;
    div.textContent = message;

    cardbody.appendChild(div);

    setTimeout(function () {
        div.remove();
    }, 2500);
}

