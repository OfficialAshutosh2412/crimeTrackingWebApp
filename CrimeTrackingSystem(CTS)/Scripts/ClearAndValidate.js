function sweetEmptyFunction() {
    Swal.fire({
        icon: "error",
        title: "Oops...",
        text: "fields should not be empty!",
    });
}

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
function AlertsMsg() {
    Swal.fire({
        icon: 'success',
        title: 'Thank you !',
        text: 'Information recorded !'
    });
}
function ClearContact() {
    $("#fullname").val("");
    $("#email").val("");
    $("#phone").val("");
    $("#purpose").val("");
    $("#details").val("");
    AlertsMsg();
}