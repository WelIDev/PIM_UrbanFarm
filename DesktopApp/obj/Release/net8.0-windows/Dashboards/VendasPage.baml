   M S B A M L   `   `   `  ����   
DesktopApp&     DesktopApp.Dashboards.VendasPage   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     R NOxyPlot.Wpf, Version=2.0.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0 oxyhttp://oxyplot.org/wpf    N� Title$  Dashboard de Vendas�� � 	Resources ��%    .���  � ColumnDefinitions �� $	��300 ��� $��* �.+��� $��0��$��0��$��10q�.+��� $��
Vendedores��$��18(�$
��Bold"�$��0,0,0,10q��� -   $��Mostrar Todas as Vendas��   MostrarVendasButtonStyle#��[  � $��0��$��10,120,10,10q�.+��� $
��Auto=�.���� -    ��Name$ VendedoresCardsControl������ .R��� -     CardButtonStyle#��[ 
 � Tag �� *
VendedorId+.���� ���#a9a9a9$��10q� �� Padding$ 15q� �� CornerRadius$ 8�����#DDDDDD$��1q�._��� .+��� $��16(�$
��Bold"����#333���� *NomeVendedor+�� $��12(����#666�� $	�ID:�� �� ��� *
VendedorId+�� $��1��$��10q�.+��� $��Relatório de Vendas��$��18(�$
��Bold"�$��0,0,0,10q���  �� Orientation$ 
Horizontal=�$��0,0,0,10q�.+��� .+��� $��Data Início��$��14(�$��0,0,0,5q�(  "System.Windows.Controls.DatePicker  -   $��
DataInicio���� $��10,0,0,0q�.+��� $��Data Fim��$��14(�$��0,0,0,5q�  -   $��DataFim��  OxyPlot.Wpf.PlotView  -     Name$ GraficoVendas��$	��400���� $��1��$��Exportar��$��Right=�$��Bottom=�$��20q�#��[  $	��100��$��40��