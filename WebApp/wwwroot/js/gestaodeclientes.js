const clienteForm = document.getElementById('clienteForm');
const clienteTableBody = document.querySelector('#clienteTable tbody');
const searchInput = document.getElementById('searchInput');
const filterColumn = document.getElementById('filterColumn');
const successNotification = document.getElementById('successNotification');
const editNotification = document.getElementById('editNotification');
const deleteNotification = document.getElementById('deleteNotification');
const dataNascimentoInput = document.getElementById('dataNascimento');
const submitButton = document.getElementById('submitButton');
let editingRow = null;

// Define a data atual no campo de data de nascimento, se a página de adicionar estiver aberta
if (dataNascimentoInput) {
    document.addEventListener('DOMContentLoaded', function () {
        const hoje = new Date().toISOString().split('T')[0];
        dataNascimentoInput.value = hoje;
    });
}

// Função para mostrar a notificação de sucesso
function showNotification(type) {
    let notification;
    switch(type) {
        case 'success':
            notification = successNotification;
            break;
        case 'edit':
            notification = editNotification;
            break;
        case 'delete':
            notification = deleteNotification;
            break;
        default:
            return;
    }
    notification.classList.remove('hidden');
    notification.classList.add('visible');

    setTimeout(function () {
        notification.classList.remove('visible');
        notification.classList.add('hidden');
    }, 3000);
}

// Função para editar um cliente
function editarCliente(button) {
    const row = button.parentElement.parentElement;
    const cells = row.querySelectorAll('td');

    document.getElementById('nomeCompleto').value = cells[0].textContent;
    document.getElementById('email').value = cells[1].textContent;
    document.getElementById('cpf').value = cells[2].textContent;
    document.getElementById('telefone').value = cells[3].textContent;
    document.getElementById('dataNascimento').value = cells[4].textContent;
    document.getElementById('rua').value = cells[5].textContent;
    document.getElementById('cep').value = cells[6].textContent;
    document.getElementById('numero').value = cells[7].textContent;
    document.getElementById('bairro').value = cells[8].textContent;
    document.getElementById('cidade').value = cells[9].textContent;
    document.getElementById('estado').value = cells[10].textContent;

    editingRow = row;

    submitButton.textContent = 'Editar Cliente';
}

// Função para excluir um cliente
function excluirCliente(button) {
    const row = button.parentElement.parentElement;
    clienteTableBody.removeChild(row);
    showNotification('delete');
}

// Função para pesquisar clientes
function pesquisarCliente() {
    const filter = searchInput.value.toUpperCase();
    const selectedColumn = filterColumn.value;
    const rows = clienteTableBody.getElementsByTagName('tr');

    for (let i = 0; i < rows.length; i++) {
        const cells = rows[i].getElementsByTagName('td');
        let match = false;

        switch (selectedColumn) {
            case 'nomeCompleto':
                match = cells[0].textContent.toUpperCase().includes(filter);
                break;
            case 'email':
                match = cells[1].textContent.toUpperCase().includes(filter);
                break;
            case 'cpf':
                match = cells[2].textContent.toUpperCase().includes(filter);
                break;
            case 'telefone':
                match = cells[3].textContent.toUpperCase().includes(filter);
                break;
            case 'dataNascimento':
                match = cells[4].textContent.toUpperCase().includes(filter);
                break;
            case 'rua':
                match = cells[5].textContent.toUpperCase().includes(filter);
                break;
            case 'cep':
                match = cells[6].textContent.toUpperCase().includes(filter);
                break;
            case 'numero':
                match = cells[7].textContent.toUpperCase().includes(filter);
                break;
            case 'bairro':
                match = cells[8].textContent.toUpperCase().includes(filter);
                break;
            case 'cidade':
                match = cells[9].textContent.toUpperCase().includes(filter);
                break;
            case 'estado':
                match = cells[10].textContent.toUpperCase().includes(filter);
                break;
        }

        rows[i].style.display = match ? '' : 'none';
    }
}