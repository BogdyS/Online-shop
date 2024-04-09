document.getElementById("submit")
    .addEventListener('click', async () => {
        let login = document.getElementById("email").value;
        let password = document.getElementById("password").value;
        let url = `${apiOptions.baseUrl}sign-in`;
        let body = {
            login: login,
            password: password
        };

        const response = await fetch(url, {
            method: "POST",
            body: JSON.stringify(body),
            headers: {
                RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val(),
                "Content-Type":"application/json"
            }
        });

        if (response.ok) {
            window.location.href = `/catalog`;
        }
        else {
            alert("Authorization failed");
        }
    });
