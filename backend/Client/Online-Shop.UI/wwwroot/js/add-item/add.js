document.addEventListener("DOMContentLoaded", function () {
	const form = document.getElementById("prod-form");
	
	form.addEventListener("submit", async function (event) {
		event.preventDefault();

		if (!validateForm()) {
			alert("Ошибка валидации");
			return;
		}

		const formData = new FormData(form);

		try {
			const response = await fetch(`${apiOptions.baseUrl}items/create`, {
				method: "POST",
				body: formData,
				headers: {
					RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
				}
			});

			if (!response.ok) {
				throw new Error(`HTTP error! Status: ${response.status}`);
			}

			const result = await response.json();
			alert("Товар успешно добавлен!");
		} catch (error) {
			console.error("Error adding product:", error);
			alert("Произошла ошибка при добавлении товара. Пожалуйста, попробуйте еще раз.");
		}
	});
});
