   M S B A M L   `   `   `  ����  
DesktopApp,    &DesktopApp.Views.Dashboards.VendasPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      R NOxyPlot.Wpf, Version=2.0.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0 oxyhttp://oxyplot.org/wpf  5        N� Title$  Dashboard de Vendas��5       � 	Resources 5      ��%    5
      .��5      �  � ColumnDefinitions 5   
   �� 5      $	��300 �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6%   $��10q�62   .+�5      �� $��
Vendedores��6   $��18(�6*   $
��Bold"�68   $��0,0,0,10q�6J   �� 5      -   5        $��Mostrar Todas as Vendas��5         MostrarVendasButtonStyle#��[  5      5      � 5   
   $��0��6   $��10,120,10,10q�6   .+�5      �� $
��Auto=�6   .��5      �� -   5         ��Name$ VendedoresCardsControl��5      ��5      �� 5      .R�5       �� -   5          CardButtonStyle#��[ 5!   %   
 � Tag 5    @   �� *
VendedorId+.��5"   "   �� ���#a9a9a9$��10q�6>    �� Padding$ 15q�6J    �� CornerRadius$ 8��5#   )   ���#DDDDDD$��1q�5$   )   ._�5%   &   �� .+�5&   *   �� $��16(�6R   $
��Bold"�5'   4   ���#333��5&   4   �� *NomeVendedor+5'   F   �� 5(   *   $��12(�64   ���#666�� 5)   .   $	�ID:��62    �� 5*   .   �62   �� *
VendedorId+5+   +   5,   '   5-   #   5.      5/      51      52      53      �� 56   
   $��1��6   $��10q�6%   .+�57      �� $��Relatório de Vendas��6   $��18(�63   $
��Bold"�6A   $��0,0,0,10q�58      �� 5;       �� Orientation$ 
Horizontal=�6   $��0,0,0,10q�62   .+�5<      �� .+�5=      �� $��Data Início��6    $��14(�63   $��0,0,0,5q�6A   (  "System.Windows.Controls.DatePicker  5>      -   5        $��
DataInicio��5>   !   5?   !   5@      �� 5A      $��10,0,0,0q�6   .+�5B      �� $��Data Fim��6    $��14(�60   $��0,0,0,5q�6>     5C      -   5        $��DataFim��5C   !   5D   !   5E      5F        OxyPlot.Wpf.PlotView  5I      -   5          Name$ GraficoVendas��5I      $	��400��62   5J      �� 5L   
   $��1��6   $��Exportar��6!   $��Right=�64   $��Bottom=�5M      $��20q�5N      #��[  6   $	��100��6O   $��40��6[   5O      5P      