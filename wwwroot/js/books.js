
function enableEdit(id) {
    const row = document.querySelector(`#row-${id}`);
    row.querySelectorAll(".display").forEach(span => span.classList.add("d-none"));
    row.querySelectorAll(".edit-input").forEach(input => input.classList.remove("d-none"));

    const buttons = row.querySelectorAll("button");
    buttons[0].classList.add("d-none"); // Editar
    buttons[1].classList.remove("d-none"); // Confirmar
    buttons[2].classList.remove("d-none"); // Cancelar
}

function cancelEdit(id) {
    const row = document.querySelector(`#row-${id}`);
    row.querySelectorAll(".display").forEach(span => span.classList.remove("d-none"));
    row.querySelectorAll(".edit-input").forEach(input => input.classList.add("d-none"));

    const buttons = row.querySelectorAll("button");
    buttons[0].classList.remove("d-none"); // Editar
    buttons[1].classList.add("d-none"); // Confirmar
    buttons[2].classList.add("d-none"); // Cancelar
}

function confirmEdit(id) {
	const row = document.querySelector(`#row-${id}`);
	const book = {
		Id: id,
		Title: row.querySelector('input[name="title"]').value,
		Description: row.querySelector('input[name="description"]').value,
		Author: row.querySelector('input[name="author"]').value
	};

	fetch('?handler=Edit', {
		method: 'POST',
		headers: {
			'Content-Type': 'application/json',
			'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
		},
		body: JSON.stringify(book)
	})
		.then(response => {
			if (!response.ok) throw new Error("Error en la actualización");
			return response.json();
		})
		.then(data => {
			if (data.success) {
				alert("Libro actualizado correctamente.");
				location.reload();
			}
		})
		.catch(error => console.error('Error al actualizar:', error));
}