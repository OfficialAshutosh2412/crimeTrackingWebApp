//navbar toggler
document.querySelector('#login-tog').addEventListener("click", () => {
    document.querySelector('.signin').classList.toggle('show-login');
});
//tools toggler
document.querySelector('#tool-toggler').addEventListener("click", () => {
    document.querySelector('.top-bar').classList.toggle('hide-top');
});
//preloader
function myfunction() {
    var load = document.getElementById("preloader");
    load.style.display = "none"
};
//gallery revealer
document.querySelector('#hide').addEventListener("click", () => {
    document.querySelector('.ind-hide').classList.toggle('ind-show');
});
//closebtn
document.querySelector('#close').addEventListener("click", () => {
    document.querySelector('.ind-hide').classList.toggle('ind-show');
});
//chandrayan gallery revealer
document.querySelector('#chandrahide').addEventListener("click", () => {
    document.querySelector('.chandra-hide').classList.toggle('chandra-show');
});
//closebtn
document.querySelector('#chandraclose').addEventListener("click", () => {
    document.querySelector('.chandra-hide').classList.toggle('chandra-show');
});
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
    //if (panel.style.visibility == "hidden") {
    //    panel.style.visibility = "visible";
    //    panel.style.opacity = "1";
    //}
    //else {
    //    panel.style.visibility = "hidden";
    //    panel.style.opacity = "0";
    //}
    panel.style.cssText = "opacity:1;visibility:visible;transform:scale(1);";
}
function guideclose() {
    let panel = document.querySelector('#guide-panel');
    //if (panel.style.visibility == "visible") {
    //    panel.style.visibility = "hidden";
    //    panel.style.opacity = "0";
    //}
    //else {
    //    panel.style.visibility = "visible";
    //    panel.style.opacity = "1";
    //}
    panel.style.cssText = "opacity:0;transform:scale(0.5);visibility:hidden;";
}
