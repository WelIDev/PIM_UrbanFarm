document.getElementById('submitButton').addEventListener('click', async function (event) {
    event.preventDefault(); // Impede o envio tradicional do formulário

    // Coletar os valores do formulário
    const nome = document.getElementById('Nome').value;
    const email = document.getElementById('Email').value;
    const cnpj = document.getElementById('Cnpj').value;
    const inscricaoEstadual = document.getElementById('InscricaoEstadual').value;
    const telefone = document.getElementById('Telefone').value;
    const rua = document.getElementById('rua').value;
    const cep = document.getElementById('cep').value;
    const numero = document.getElementById('numero').value;
    const bairro = document.getElementById('bairro').value;
    const cidade = document.getElementById('cidade').value;
    const estado = document.getElementById('estado').value;

    // Validação dos campos obrigatórios
    if (!nome || !email || !cnpj || !inscricaoEstadual || !telefone || !rua || !cep || !numero || !cidade || !estado) {
        alert('Todos os campos obrigatórios devem ser preenchidos.');
        return;
    }

    // Monta o objeto fornecedor a ser enviado
    const fornecedor = {
        Id: 0,
        Nome: nome,
        Email: email,
        Cnpj: cnpj,
        InscricaoEstadual: inscricaoEstadual,
        Telefone: telefone,
        EnderecoId: 0,
        Endereco: {
            Id: 0,
            Rua: rua,
            Cep: cep,
            Numero: numero,
            Bairro: bairro,
            Cidade: cidade,
            Estado: estado
        }
    };
console.log(JSON.stringify(fornecedor))
    
    try {
        const response = await fetch('/Fornecedor/AdicionarFornecedor', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(fornecedor)
        });

        if (response.ok) {
            // Se a resposta for bem-sucedida, redireciona para a página de gestão de fornecedores
            alert('Fornecedor adicionado com sucesso!');
            window.location.href = '/Fornecedor/GestaoDeFornecedores';
        } else {
            // Caso o servidor retorne um erro
            alert('Erro ao adicionar fornecedor.');
        }
    } catch (error) {
        alert('Erro na requisição: ' + error.message);
    }
});

// Função para editar um fornecedor
function editarFornecedor(button) {
    const row = button.closest('tr');
    const cells = row.getElementsByTagName('td');

    document.getElementById('Nome').value = cells[0].textContent;
    document.getElementById('Email').value = cells[1].textContent;
    document.getElementById('Cnpj').value = cells[2].textContent;
    document.getElementById('InscricaoEstadual').value = cells[3].textContent;
    document.getElementById('Telefone').value = cells[4].textContent;
    document.getElementById('rua').value = cells[5].textContent;
    document.getElementById('cep').value = cells[6].textContent;
    document.getElementById('numero').value = cells[7].textContent;
    document.getElementById('bairro').value = cells[8].textContent;
    document.getElementById('cidade').value = cells[9].textContent;
    document.getElementById('estado').value = cells[10].textContent;

    // Altera o botão de envio para "Editar Fornecedor"
    document.getElementById('submitButton').textContent = 'Editar Fornecedor';

    // Salva a linha a ser editada (opcional, caso precise fazer uma atualização no servidor)
    editingRow = row;
}

// Função para excluir um fornecedor
function excluirFornecedor(button) {
    const row = button.closest('tr');
    row.remove();
    alert('Fornecedor excluído!');
}

// Função para pesquisar fornecedores
function pesquisarfornecedor() {
    const searchInput = document.getElementById('searchInput');
    const filter = searchInput.value.toUpperCase();
    const filterColumn = document.getElementById('filterColumn');
    const selectedColumn = filterColumn.value;
    const fornecedorTableBody = document.getElementById('fornecedorTableBody');
    const rows = fornecedorTableBody.getElementsByTagName('tr');

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
            case 'Cnpj':
                match = cells[2].textContent.toUpperCase().includes(filter);
                break;
            case 'InscricaoEstadual':
                match = cells[3].textContent.toUpperCase().includes(filter);
                break;
            case 'Telefone':
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

// Função para alternar a visibilidade da sidebar
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

// Fechar a sidebar ao clicar fora dela
window.onclick = function (event) {
    const sidebar = document.querySelector('.sidebar');
    if (!event.target.matches('.menu-icon') && sidebar.style.left === '0px') {
        sidebar.style.left = '-250px'; // Esconder a sidebar
        document.querySelector('.main-content').style.marginLeft = '0'; // Ajustar o conteúdo principal
    }
};

// Alternar o menu de perfil
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
