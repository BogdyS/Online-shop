const inputFields = document.querySelectorAll(".new-prod-wrap input");
const textFields = document.querySelectorAll(".new-prod-wrap textarea");

inputFields.forEach((input) => {
  input.addEventListener("focus", () => {
    const tooltip = input.nextElementSibling;
    tooltip.style.display = "block";
  });

  input.addEventListener("blur", () => {
    const tooltip = input.nextElementSibling;
    tooltip.style.display = "none";
  });
});

textFields.forEach((textarea) => {
  textarea.addEventListener("focus", () => {
    const tooltip = textarea.nextElementSibling;
    tooltip.style.display = "block";
  });

  textarea.addEventListener("blur", () => {
    const tooltip = textarea.nextElementSibling;
    tooltip.style.display = "none";
  });
});
