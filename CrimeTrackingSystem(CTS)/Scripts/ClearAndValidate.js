//globals
function sweetEmptyFunction() {
    Swal.fire({
        icon: "error",
        title: "Oops...",
        text: "fields should not be empty!",
    });
}
function AlertsMsg() {
    Swal.fire({
        icon: 'success',
        title: 'Thank you !',
        text: 'Information recorded !'
    });
}

function ValidateImage() {
    let image = $("#image");
    if (image[0].files.length != 0) {
        //get filename with path
        let filename = image.val();
        //getting extension
        let extension = filename.substring(filename.lastIndexOf('.')).toLowerCase();
        if (extension == '.png' || extension == '.jpeg' || extension == '.jpg') {
            let fileSize = image[0].files[0].size;
            if (fileSize < 1024 * 1024) {
                return true;
            }
            else {
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: "file is larger than 1MB..",
                });
            }
        } else {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "file type not supported..",
            });
        }
    }
    else {
        Swal.fire({
            icon: "error",
            title: "Oops...",
            text: "choose an image..",
        });
    }
}
//contact us
function ValidateContact() {
    let fullname = $("#fullname").val();
    let email = $("#email").val();
    let phone = $("#phone").val();
    let purpose = $("#purpose").val();
    let details = $("#details").val();
    if (fullname === "" || email === "" || phone === "" || purpose === "" || details === "") {
        sweetEmptyFunction();
        return false;
    }
}

function ClearContact() {
    $("#fullname").val("");
    $("#email").val("");
    $("#phone").val("");
    $("#purpose").val("");
    $("#details").val("");
    AlertsMsg();
}
//feedback
function validationCheck() {
    let name = $("#name").val();
    let email = $("#email").val();
    let words = $("#words").val();
    if (name === "" || email === "" || words === "") {
        sweetEmptyFunction()
        return false;
    }
}
function ClearFeedbackFields() {
    AlertsMsg();
    $("#name").val("");
    $("#email").val("");
    $("#words").val("");
}
//signup
function signupCheck() {
    let username = $("#username").val();
    let password = $("#password").val();
    let email = $("#email").val();
    let pincode = $("#pincode").val();
    let gender = $("#gender").val();
    let address = $("#address").val();
    let mstatus = $("#mstatus").val();
    let lstatus = $("#lstatus").val();
    let adhaar = $("#adhaar").val();
    let phone = $("#phone").val();
    let role = $("#role").val();
    if (username == "" || password == "" || email == "" || pincode == "" || gender === "" || address == "" || mstatus === "" || lstatus === "" || adhaar == "" || phone == "" || role == "") {
        sweetEmptyFunction();
        return false;
    }
    ValidateImage();
}
function SignupSuccess() {
    Swal.fire({
        icon: 'success',
        title: 'Thank you !',
        text: `You're a member now !`,
        footer: '<a href="/Home/Login">Click here to login</a>'
    });
    $("#username").val('');
    $("#password").val('');
    $("#email").val('');
    $("#pincode").val('');
    $("#gender").val('');
    $("#address").val('');
    $("#mstatus").val('');
    $("#lstatus").val('');
    $("#adhaar").val('');
    $("#phone").val('');
    $("#role").val('');
}