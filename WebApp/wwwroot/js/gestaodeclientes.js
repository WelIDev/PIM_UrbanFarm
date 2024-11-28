document.addEventListener('DOMContentLoaded', function () {
    const clienteForm = document.getElementById('clienteForm');
    const clienteTableBody = document.querySelector('#clienteTable tbody');
    const searchInput = document.getElementById('searchInput');
    const filterColumn = document.getElementById('filterColumn');
    const dataNascimentoInput = document.getElementById('dataNascimento');
    const submitButton = document.getElementById('submitButton');
    let editingRow = null;

    // Define a data atual no campo de data de nascimento
    if (dataNascimentoInput) {
        const hoje = new Date().toISOString().split('T')[0];
        dataNascimentoInput.value = hoje;
    }

    showTab('adicionar');

    // Função para adicionar um novo cliente na tabela
    if (clienteForm) {
        clienteForm.addEventListener('submit', function (event) {
            event.preventDefault();

            const nome = document.getElementById('Nome').value;
            const email = document.getElementById('Email').value;
            const cpfcnpj = document.getElementById('CpfCnpj').value;
            const telefone = document.getElementById('Telefone').value;
            const dataNascimento = dataNascimentoInput ? dataNascimentoInput.value : '';
            const rua = document.getElementById('rua').value;
            const cep = document.getElementById('cep').value;
            const numero = document.getElementById('numero').value;
            const bairro = document.getElementById('bairro').value;
            const cidade = document.getElementById('cidade').value;
            const estado = document.getElementById('estado').value;

            if (!nome || !email || !cpfcnpj || !telefone || !dataNascimento || !rua || !cep || !numero || !cidade || !estado) {
                alert('Todos os campos obrigatórios devem ser preenchidos.');
                return;
            }

            if (editingRow) {
                editingRow.innerHTML =
                    `<td data-label="Nome Completo">${nome}</td>
                    <td data-label="E-mail">${email}</td>
                    <td data-label="CPF">${cpfcnpj}</td>
                    <td data-label="Telefone">${telefone}</td>
                    <td data-label="Data de Nascimento">${dataNascimento}</td>
                    <td data-label="Rua">${rua}</td>
                    <td data-label="CEP">${cep}</td>
                    <td data-label="Número">${numero}</td>
                    <td data-label="Bairro">${bairro}</td>
                    <td data-label="Cidade">${cidade}</td>
                    <td data-label="Estado">${estado}</td>
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
                    `<td data-label="Nome Completo">${nome}</td>
                    <td data-label="E-mail">${email}</td>
                    <td data-label="CPF">${cpfcnpj}</td>
                    <td data-label="Telefone">${telefone}</td>
                    <td data-label="Data de Nascimento">${dataNascimento}</td>
                    <td data-label="Rua">${rua}</td>
                    <td data-label="CEP">${cep}</td>
                    <td data-label="Número">${numero}</td>
                    <td data-label="Bairro">${bairro}</td>
                    <td data-label="Cidade">${cidade}</td>
                    <td data-label="Estado">${estado}</td>
                    <td data-label="Ações">
                        <button class="editar" onclick="editarCliente(this)">Editar</button>
                        <button class="excluir" onclick="excluirCliente(this)">Excluir</button>
                    </td>`;
                if (clienteTableBody) {
                    clienteTableBody.appendChild(newRow);
                }
                clienteForm.reset();
                showNotification('success');
            }
        });
    }

    // Função para alternar a visibilidade da sidebar
    window.toggleSidebar = function () {
        const sidebar = document.querySelector('.sidebar');
        const mainContent = document.querySelector('.main-content');

        if (sidebar && mainContent) {
            sidebar.classList.toggle('open'); // Alterna a classe 'open' na sidebar
            mainContent.classList.toggle('shifted'); // Alterna a classe 'shifted' no conteúdo principal
        } else {
            console.error('Erro: .sidebar ou .main-content não encontrados no DOM.');
        }
    };

    // Função para alternar o menu de perfil
    window.toggleProfileMenu = function (event) {
        const profileMenu = document.querySelector('.profile-menu');

        // Alterna a visibilidade do menu de perfil
        profileMenu.style.display = profileMenu.style.display === 'block' ? 'none' : 'block';
        event.stopPropagation(); // Impede a propagação do clique para a janela
    };
});

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

function showNotification(type) {
    const notification = document.createElement('div');
    notification.className = `notification ${type}`;
    notification.textContent = type === 'success' ? 'Cliente adicionado com sucesso!' :
        type === 'edit' ? 'Cliente editado com sucesso!' :
            type === 'delete' ? 'Cliente excluído com sucesso!' : '';

    document.body.appendChild(notification);
    setTimeout(() => notification.remove(), 3000);
}

async function buscarEndereco() {
    const cep = document.getElementById('cep').value.trim();

    try {
        const response = await fetch(`@Url.Action("ObterEndereco", "Cliente")?cep=${cep}`);
        const { rua, bairro, cidade, estado } = await response.json();

        document.getElementById('rua').value = rua || '';
        document.getElementById('bairro').value = bairro || '';
        document.getElementById('cidade').value = cidade || '';
        document.getElementById('estado').value = estado || '';
    } catch (error) {
        alert(error.message);
    }
}
