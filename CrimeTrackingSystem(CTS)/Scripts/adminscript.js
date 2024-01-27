//navbar
//navbar active link
const pathName = window.location.pathname;
const pageName = pathName.split("/").pop();
if (pageName === "Admin" || pageName === "admin") {
    document.querySelector('#adminhome').classList.add("admin-nav-active");
}
if (pageName === "RecordRoom" || pageName === "recordroom") {
    document.querySelector('#adminrecord').classList.add("admin-nav-active");
}
if (pageName === "login" || pageName === "Login") {
    document.querySelector('#loginclick').classList.add("admin-nav-active");
}
if (pageName === "policestationentry" || pageName === "PoliceStationEntry") {
    document.querySelector('#PoliceStationEntry').classList.add("admin-nav-active");
}
if (pageName === "contactrequests" || pageName === "ContactRequests") {
    document.querySelector('#ContactRequests').classList.add("admin-nav-active");
}
if (pageName === "faqrecords" || pageName === "FaqRecords") {
    document.querySelector('#FaqRecords').classList.add("admin-nav-active");
}
if (pageName === "newsrecord" || pageName === "NewsRecord") {
    document.querySelector('#NewsRecord').classList.add("admin-nav-active");
}
// initials records css
document.querySelector('#crimeBtn').classList.add("bg-warning");
document.querySelector('#crimeBtn').classList.add("text-dark");
document.querySelector('#crimeComplain').classList.add("border");
document.querySelector('#crimeComplain').classList.add("border-warning");
document.querySelector('#generalprint').classList.add("d-none");
document.querySelector('#personprint').classList.add("d-none");
document.querySelector('#valueprint').classList.add("d-none");


//box IDs
let crimebox = document.querySelector('#crimeComplain');
let generalbox = document.querySelector('#generalComplain');
let personbox = document.querySelector('#missingPerson');
let valuebox = document.querySelector('#missingValue');

//button IDs for toggle data
let general = document.querySelector('#generalBtn');
let crime = document.querySelector('#crimeBtn');
let person = document.querySelector('#personBtn');
let valuable = document.querySelector('#valueBtn');

//button IDs for print data
let pcrime = document.querySelector('#crimeprint');
let pgeneral = document.querySelector('#generalprint');
let pperson = document.querySelector('#personprint');
let pvalue = document.querySelector('#valueprint');


//general btn click event
general.addEventListener('click', () => {
    general.classList.add("bg-warning", "text-dark");
    generalbox.classList.add("border", "border-warning");
    crime.classList.add("btn-outline-warning");
    crimebox.classList.add("hidden-table");
    personbox.classList.add("hidden-table");
    valuebox.classList.add("hidden-table");
    pcrime.classList.add("d-none");
    pperson.classList.add("d-none");
    pvalue.classList.add("d-none");

    crime.classList.remove("bg-warning", "text-dark");
    person.classList.remove("bg-warning", "text-dark");
    valuable.classList.remove("bg-warning", "text-dark");
    generalbox.classList.remove("hidden-table");
    pgeneral.classList.remove("d-none");
});
//crime btn click event
crime.addEventListener('click', () => {
    crime.classList.add("bg-warning", "text-dark");
    general.classList.add("btn-outline-warning");
    generalbox.classList.add("hidden-table");
    pgeneral.classList.add("d-none");
    pperson.classList.add("d-none");
    pvalue.classList.add("d-none");
    crimebox.classList.add("border", "border-warning");
    valuebox.classList.add("hidden-table");
    generalbox.classList.add("hidden-table");
    personbox.classList.add("hidden-table");

    crimebox.classList.remove("hidden-table");
    general.classList.remove("bg-warning", "text-dark");
    person.classList.remove("bg-warning", "text-dark");
    valuable.classList.remove("bg-warning", "text-dark");
    pcrime.classList.remove("d-none");
});
//person btn click event
person.addEventListener('click', () => {
    person.classList.add("bg-warning", "text-dark");
    valuebox.classList.add("hidden-table");
    generalbox.classList.add("hidden-table");
    crimebox.classList.add("hidden-table");
    pcrime.classList.add("d-none");
    pgeneral.classList.add("d-none");
    pvalue.classList.add("d-none");

    general.classList.remove("bg-warning", "text-dark");
    crime.classList.remove("bg-warning", "text-dark");
    valuable.classList.remove("bg-warning", "text-dark");
    personbox.classList.remove("hidden-table");
    pperson.classList.remove("d-none");
});
//value btn click event
valuable.addEventListener('click', () => {
    valuable.classList.add("bg-warning", "text-dark");

    generalbox.classList.add("hidden-table");
    crimebox.classList.add("hidden-table");
    personbox.classList.add("hidden-table");
    pcrime.classList.add("d-none");
    pgeneral.classList.add("d-none");
    pperson.classList.add("d-none");

    general.classList.remove("bg-warning", "text-dark");
    crime.classList.remove("bg-warning", "text-dark");
    person.classList.remove("bg-warning", "text-dark");
    valuebox.classList.remove("hidden-table");
    pvalue.classList.remove("d-none");
});

//printing crime data
pcrime.addEventListener('click', () => {
    var printWindow = window.open('', '_blank');
    let printing = crimebox;
    if (printing) {
        printWindow.document.write(crimebox.outerHTML);
        printWindow.document.write('</body></html>');
        printWindow.document.close();
        printWindow.print();
    } else {
        console.error("content not found");
    }
});

//printing general data
pgeneral.addEventListener('click', () => {
    var printWindow = window.open('', '_blank');
    let printing = generalbox;
    if (printing) {
        printWindow.document.write(generalbox.outerHTML);
        printWindow.document.write('</body></html>');
        printWindow.document.close();
        printWindow.print();
    } else {
        console.error("conent not found");
    }
});

//printing missing person data
pperson.addEventListener('click', () => {
    var printWindow = window.open('', '_blank');
    printWindow.document.write('<style type="text/css"> @page { size: landscape; } </style>');
    let printing = personbox;
    if (printing) {
        printWindow.document.write(personbox.outerHTML);
        printWindow.document.write('</body></html>');
        printWindow.document.close();
        printWindow.print();
    } else {
        console.error("conent not found");
    }
});

//printing missing valuable data
pvalue.addEventListener('click', () => {
    var printWindow = window.open('', '_blank');
    printWindow.document.write('<style type="text/css"> @page { size: landscape; } </style>');
    let printing = valuebox;
    if (printing) {
        printWindow.document.write(valuebox.outerHTML);
        printWindow.document.write('</body></html>');
        printWindow.document.close();
        printWindow.print();
    } else {
        console.error("conent not found");
    }
});
//printing police station data
function stationListPrint() {
    let policedata = document.querySelector('#policestationdata');
    var printWindow = window.open('', '_blank');
    printWindow.document.write('<style type="text/css"> @page { size: landscape; } </style>');
    let printing = policedata;
    if (printing) {
        printWindow.document.write(policedata.outerHTML);
        printWindow.document.write('</body></html>');
        printWindow.document.close();
        printWindow.print();
    } else {
        console.error("content not found");
    }
}
//printing Faq Records
function FaqRecordPrint() {
    let policedata = document.querySelector('#faqRecord');
    var printWindow = window.open('', '_blank');
    var stylesheets = document.styleSheets;
    for (var i = 0; i < stylesheets.length; i++) {
        printWindow.document.write('<link rel="stylesheet" type="text/css" href="' + stylesheets[i].href + '">');
    }
    printWindow.document.write('<style type="text/css"> @page { size: landscape; } </style>');
    let printing = policedata;
    if (printing) {
        printWindow.document.write(policedata.outerHTML);
        printWindow.document.write('</body></html>');
        printWindow.document.close();
        printWindow.print();
    } else {
        console.error("content not found");
    }
}
//printing News Records
function NewsRecordPrint() {
    let policedata = document.querySelector('#newsData');
    var printWindow = window.open('', '_blank');
    var stylesheets = document.styleSheets;
    for (var i = 0; i < stylesheets.length; i++) {
        printWindow.document.write('<link rel="stylesheet" type="text/css" href="' + stylesheets[i].href + '">');
    }
    printWindow.document.write('<style type="text/css"> @page { size: landscape; } </style>');
    let printing = policedata;
    if (printing) {
        printWindow.document.write(policedata.outerHTML);
        printWindow.document.write('</body></html>');
        printWindow.document.close();
        printWindow.print();
    } else {
        console.error("content not found");
    }
}