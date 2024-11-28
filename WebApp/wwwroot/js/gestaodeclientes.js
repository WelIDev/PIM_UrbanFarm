const clienteForm = document.getElementById('clienteForm');
const clienteTableBody = document.querySelector('#clienteTable tbody');
const searchInput = document.getElementById('searchInput');
const filterColumn = document.getElementById('filterColumn');
const dataNascimentoInput = document.getElementById('dataNascimento');
const submitButton = document.getElementById('submitButton');
let editingRow = null;

// Define a data atual no campo de data de nascimento
document.addEventListener('DOMContentLoaded', function () {
    const hoje = new Date().toISOString().split('T')[0];
    dataNascimentoInput.value = hoje;

    showTab('adicionar');
});

// Função para adicionar um novo cliente na tabela
clienteForm.addEventListener('submit', function (event) {
    event.preventDefault();

    const nomeCompleto = document.getElementById('Nome').value;
    const email = document.getElementById('Email').value;
    const cpf = document.getElementById('CpfCnpj').value;
    const telefone = document.getElementById('Telefone').value;
    const dataNascimento = dataNascimentoInput.value;
    const rua = document.getElementById('rua').value;
    const cep = document.getElementById('cep').value;
    const numero = document.getElementById('numero').value;
    const bairro = document.getElementById('bairro').value;
    const cidade = document.getElementById('cidade').value;
    const estado = document.getElementById('estado').value; // Agora é um select

    if (!Nome || !Email || !CpfCnpj || !Telefone || !dataNascimento || !rua || !cep || !numero || !cidade || !estado) {
        alert('Todos os campos obrigatórios devem ser preenchidos.');
        return;
    }

    if (editingRow) {
        editingRow.innerHTML =
            `<td data-label="Nome Completo">${Nome}</td>
            <td data-label="E-mail">${Email}</td>
            <td data-label="CPF">${CpfCnpj}</td>
            <td data-label="Telefone">${Telefone}</td>
            <td data-label="Data de Nascimento">${dataNascimento}</td>
            <td data-label="Rua">${rua}</td>
            <td data-label="CEP">${cep}</td>
            <td data-label="Número">${numero}</td>
            <td data-label="Bairro">${bairro}</td>
            <td data-label="Cidade">${cidade}</td>
            <td data-label="Estado">${estado}</td> <!-- Exibindo o estado selecionado -->
            <td data-label="Ações">
                <button class="editar" onclick="editarCliente(this)">Editar</button>
                <button class="excluir" onclick="excluirCliente(this)">Excluir</button>
            </td>`;

        editingRow = null;
        submitButton.textContent = 'Adicionar Cliente';
        submitButton.classList.remove('edit-button');
        showNotification('edit');
        showTab('visualizacao');
        clienteForm.reset();
    } else {
        const newRow = document.createElement('tr');
        newRow.innerHTML =
            `<td data-label="Nome Completo">${Nome}</td>
            <td data-label="E-mail">${Email}</td>
            <td data-label="CPF">${CpfCnpj}</td>
            <td data-label="Telefone">${Telefone}</td>
            <td data-label="Data de Nascimento">${dataNascimento}</td>
            <td data-label="Rua">${rua}</td>
            <td data-label="CEP">${cep}</td>
            <td data-label="Número">${numero}</td>
            <td data-label="Bairro">${bairro}</td>
            <td data-label="Cidade">${cidade}</td>
            <td data-label="Estado">${estado}</td> <!-- Exibindo o estado selecionado -->
            <td data-label="Ações">
                <button class="editar" onclick="editarCliente(this)">Editar</button>
                <button class="excluir" onclick="excluirCliente(this)">Excluir</button>
            </td>`;

        clienteTableBody.appendChild(newRow);
        clienteForm.reset();
        showNotification('success');
    }
});

// Função para editar um cliente
function editarCliente(button) {
    const row = button.parentElement.parentElement;
    const cells = row.querySelectorAll('td');

    document.getElementById('Nome').value = cells[0].textContent;
    document.getElementById('Email').value = cells[1].textContent;
    document.getElementById('CpfCnpj').value = cells[2].textContent;
    document.getElementById('Telefone').value = cells[3].textContent;
    document.getElementById('dataNascimento').value = cells[4].textContent;
    document.getElementById('rua').value = cells[5].textContent;
    document.getElementById('cep').value = cells[6].textContent;
    document.getElementById('numero').value = cells[7].textContent;
    document.getElementById('bairro').value = cells[8].textContent;
    document.getElementById('cidade').value = cells[9].textContent;
    document.getElementById('estado').value = cells[10].textContent; // Ajuste para preencher o estado

    editingRow = row;

    submitButton.textContent = 'Editar Cliente';
    submitButton.classList.add('edit-button');

    showTab('adicionar');
}

// Função para excluir um cliente sem confirmação
function excluirCliente(button) {
    // Exibe uma janela de confirmação
    const confirmarExclusao = confirm("Você realmente deseja excluir esse cliente?");

    if (confirmarExclusao) {
        const row = button.parentElement.parentElement;
        clienteTableBody.removeChild(row);
        showNotification('delete');
    }
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
            case 'Nome':
                match = cells[0].textContent.toUpperCase().includes(filter);
                break;
            case 'Email':
                match = cells[1].textContent.toUpperCase().includes(filter);
                break;
            case 'CpfCnpj':
                match = cells[2].textContent.toUpperCase().includes(filter);
                break;
            case 'Telefone':
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

// Função para mostrar uma aba
function showTab(tabId) {
    const tabs = document.querySelectorAll('.tab');
    tabs.forEach(tab => {
        if (tab.id === tabId) {
            tab.style.display = 'block';
        } else {
            tab.style.display = 'none';
        }
    });

    const buttons = document.querySelectorAll('.tab-button');
    buttons.forEach(button => {
        button.classList.toggle('active', button.getAttribute('onclick').includes(tabId));
    });
}

function toggleSidebar() {
    const sidebar = document.querySelector('.sidebar');
    const mainContent = document.querySelector('.main-content');

    if (sidebar.style.left === '0px') {
        sidebar.style.left = '-250px'; // Esconder a sidebar
        mainContent.style.marginLeft = '0'; // Ajustar o conteúdo principal
    } else {
        sidebar.style.left = '0'; // Mostrar a sidebar
        mainContent.style.marginLeft = '250px'; // Ajustar o conteúdo principal
    }
}

// Fechar o menu ao clicar fora dele
window.onclick = function (event) {
    const sidebar = document.querySelector('.sidebar');
    if (!event.target.matches('.menu-icon') && sidebar.style.left === '0px') {
        sidebar.style.left = '-250px'; // Esconder a sidebar
        document.querySelector('.main-content').style.marginLeft = '0'; // Ajustar o conteúdo principal
    }
};

//profile itens
function toggleProfileMenu(event) {
    const profileMenu = document.querySelector('.profile-menu');

    // Alterna a visibilidade do menu de perfil
    profileMenu.style.display = profileMenu.style.display === 'block' ? 'none' : 'block';
    event.stopPropagation(); // Impede a propagação do clique para a janela
}

// Função para permitir apenas números
function allowOnlyNumbers(event) {
    const regex = /^[0-9]*$/;
    if (!regex.test(event.target.value)) {
        event.target.value = event.target.value.replace(/\D/g, '');
    }
}

// Função para permitir apenas letras
function allowOnlyLetters(event) {
    const regex = /^[A-Za-zÀ-ÿ\s]*$/;
    if (!regex.test(event.target.value)) {
        event.target.value = event.target.value.replace(/[^A-Za-zÀ-ÿ\s]/g, '');
    }
}

