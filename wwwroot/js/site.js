// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function refresh() {
    if (window.location.pathname === "/Nodes") {
        $.get("Nodes", function(result) {
            window.location.reload();
        });
    }
}

setInterval(refresh, 3000);