   M S B A M L   `   `   `  ����P  LDiscountCalculatorGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:DiscountCalculatorGUIDiscountCalculatorGUI  5   	   &     DiscountCalculatorGUI.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:DiscountCalculatorGUI     5   	   U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   $	��350��5	   	   $	��400��5
   	     � 
ResizeMode$  NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	    � DataContext 5   	   �� *	ViewModel+ �� RelativeSource �� *self+ � 	Resources 5         tbStyle �� 
TargetType �� Property �� Value��%�   &	        �� 5   
   " ��6    .�5      ��  � ��$) #./Resources/#Champagne & Limousines)�6+   �� 5       � ��$ 20(�6)   �� 5       �@��TextAlignment$ Center=�6.   5      5      .��5      � -   5        ���	#FFFFF7F7	 � ColumnDefinitions	 5   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5    
   � -   5        
 �Name$
 
DrawerGrid��5       $��0��5!      $
��Left=�5"      ���	#FFFFCFCF.+�5$      ��  �� Orientation$ Vertical=�6   .+�5%      �� $	��150��6   $+��%./Resources/icons8-calculator-100.png��5&      $
��None=�5'      �� 5(      -   5         ��Name$ lvSales��5(      ���	#FF5F5F5F$)��#./Resources/#Champagne & Limousines)�5*      $��18(�5+       �� SelectedIndex$ 0��5,      .@�5.      �� $��50��6#   $��0q�5/   #    50   #   �� *
Sales[0]+.��51      �� $ 
Horizontal=�6%   $
��10,0q�52   %   .+�53      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ CardGiftcard=�66   ���	#FF8889B9$��Center=�55   6   $��25��56   6   $��25��57   6   �� 58      $��Certificate��6(   $��Center=�59   (   $
��10,0q�5:   (   
���   �66   5;      5<      �� 5?      $��50��6#   $��0q�5@   #    5A   #   �� *
Sales[1]+.��5B      �� $ 
Horizontal=�6%   $
��10,0q�5C   %   .+�5D        $ PercentOutline=�66   ���	#FFEF9922$��Center=�5F   6   $��25��5G   6   $��25��5H   6   �� 5I      $��Percent sale��6(   $��Center=�5J   (   $
��10,0q�5K   (   
���   �66   5L      5M      5N      5O      5P      �� 5U   
   -   5         ��Name$ btnApplicationClose��5U      $��1��5V      $��Right=�5W      $	��Top=�5X        MaterialDesignToolButton#��[ 5[      $��30��5\      $��30��5]      ��5Y      Z� ��5Z      Z� .��5_        $ Clear=�6&   ���	#FF5F5F5F5`   &   5a      � 5c   
   -   5        $
 ContentGrid��5c      $��1��5d      $��0,30,0,0q�5e      ���	#FFFFF7F7 � RowDefinitions 5f      �� 5g      $��0.05* �6    �� 5h      $��* �6    �� 5i      $
��0.2* �6    �� 5j      $��* �6    �� 5k      $
��0.2* �6    �� 5l      $��* �6    �� 5m      $��0.05* �6    .+�5q      �� $ Vertical=�6   $��Center=�5r      $��1��5s      $��Center=�5t      .+�5u      �� $��Initial cost��6   #��[  5v      �� 5w      $ 
Horizontal=�6   .+�5x      ��  �� TextAlignment$ Right=�5y      $	��150��5z      $)��#./Resources/#Champagne & Limousines)�5{      $��20(�5|      ��5x      ��  �� ElementName$ lvSales�� �� Path$* $SelectedItem.DataContext.InitialCost� �� ValidatesOnDataErrors . 5|      �� 5}      $	��rub��6    $
��10,0q�5~       $��Center=�5       #��[  5�       5�      5�      �� 5�      $ Vertical=�6   $��Center=�5�      $��Center=�5�      $��3��5�      .+�5�      �� $��Discount��6   #��[  5�      �� 5�      $ 
Horizontal=�6   .+�5�      �� $ Right=�5�      $	��150��5�      $)��#./Resources/#Champagne & Limousines)�5�      $��20(�5�      ��5�      �� $ lvSales��$' !SelectedItem.DataContext.Discount� . 5�      �� 5�      $
��10,0q�5�       $��Center=�5�       #��[  5�       ��5�       �� $ lvSales��$. (SelectedItem.DataContext.DiscountMeasure�5�       5�      5�      �� 5�      $ Vertical=�6   $��Center=�5�      $��Center=�5�      $��5��5�      .+�5�      �� $��
Final cost��6   #��[  5�      �� 5�      $ 
Horizontal=�6   .+�5�      �� $ Right=�5�      $	��150��5�      $)��#./Resources/#Champagne & Limousines)�5�      $��20(�5�       �� 
IsReadOnly . ��5�      �� $ lvSales��$( "SelectedItem.DataContext.FinalCost� �� Mode$ OneWay=� �� StringFormat$
 0.00��5�      �� 5�      $	��rub��6    $
��10,0q�5�       $��Center=�5�       #��[  5�       5�      5�      5�      5�      5�      