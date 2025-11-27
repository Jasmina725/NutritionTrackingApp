const apiBase = "https://localhost:7152/api/users";

$(document).ready(function () {
    $("#btnLogin").click(async function () {
        const username = $("#username").val().trim();
        const password = $("#password").val().trim();
        const errorDiv = $("#loginError");

        errorDiv.addClass("d-none").text("");

        if (!username|| !password) {
            errorDiv.removeClass("d-none").text("Please enter username and password.");
            return;
        }

        try {
            const response = await $.ajax({
                url: `${apiBase}/login`,
                method: "POST",
                contentType: "application/json",
                data: JSON.stringify({ username: username, password: password })
            });
            if (response && response.token) {
                localStorage.setItem("jwtToken", response.token);

                window.location.href = "/food_entries.html";
            } else {
                errorDiv.removeClass("d-none").text("Login failed: invalid response from server.");
            }
        } catch (err) {
            let msg = "Login failed. Please check your credentials.";
            if (err.responseJSON && err.responseJSON.message) {
                msg = err.responseJSON.message;
            }
            errorDiv.removeClass("d-none").text(msg);
        }
    });
});
