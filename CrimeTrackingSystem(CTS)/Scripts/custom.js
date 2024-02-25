//tools toggler
document.querySelector('#tool-toggler').addEventListener("click", () => {
    document.querySelector('.top-bar').classList.toggle('hide-top');
});
//preloader
function myfunction() {
    var load = document.getElementById("preloader");
    load.style.display = "none"
};
//faq toggler
var acc = document.getElementsByClassName("cts-accordion");
for (var i = 0; i < acc.length; i++) {
    acc[i].addEventListener("click", function () {
        this.classList.toggle('active');
        this.parentElement.classList.toggle('active');
        var pannel = this.nextElementSibling;
        if (pannel.style.display === "block") {
            pannel.style.display = "none";
        }
        else {
            pannel.style.display = "block";
        }
    });
}
//feedback
function showFeed() {
    document.querySelector('.feedback').classList.toggle('show-feeds');
}
//theme panel hidden
function showTheme() {
    document.querySelector('.themes').classList.toggle('theme-hide');
    //auto hide theme panel
}
//show setting
function showSetting() {
    document.querySelector('.themeSwitch').classList.toggle('showtheme');
    document.querySelector('.translator').classList.toggle('showtranslate');
    document.querySelector('.feedbackBtn').classList.toggle('showfeed');
    document.querySelector('.whats').classList.toggle('showwhats');
}
//instruction
function guide() {
    let panel = document.querySelector('#guide-panel');
    panel.style.cssText = "opacity:1;visibility:visible;transform:scale(1);";
}
function guideclose() {
    let panel = document.querySelector('#guide-panel');
    panel.style.cssText = "opacity:0;transform:scale(0.5);visibility:hidden;";
}

//gallery
function showGallery() {
    let box = document.querySelector('#gallery-list');
    box.style.cssText = "display:block;";
}
function hideGallery() {
    let box = document.querySelector('#gallery-list');
    box.style.cssText = "display:none;";
}

//signup toggler
function ShowSignup() {
    let box = document.querySelector(".HideSignup");
    box.classList.add("ShowSignup")
}

function HideSignup() {
    let box = document.querySelector(".HideSignup");
        box.classList.remove("ShowSignup")
}
//profile form toggler
function ShowForm() {
    let box = document.querySelector(".hiddenProfileForm");
    box.classList.add("ShowProfileForm")
}

function HideForm() {
    let box = document.querySelector(".hiddenProfileForm");
    box.classList.remove("ShowProfileForm")
}
