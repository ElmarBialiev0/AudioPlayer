   M S B A M L   `   `   `  ����F  BAudioPlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:AudioPlayerAudioPlayer  5   	       AudioPlayer.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   _ [MaterialDesignThemes.Wpf, Version=4.8.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:AudioPlayer     5   	     � Title$  AudioPlayer��5	   	   $	��500��6   $	��800��6*   .��5
      �  � ColumnDefinitions 5   
   �� 5      $
��136* �6   �� 5      $
��111* �6   �� 5      $
��133* �6   �� 5      $
��113* �6   �� 5      $
��117* �6   �� 5      $
��190* �6    � RowDefinitions 5   
   �� 5      $	��39* �6   �� 5      $
��136* �6   �� 5      $	��34* �6   �� 5      $	��33* �6   .+�5   
   r� -   5         r�Name$ media��5       r� LoadedBehavior$ Manual=�6&   $��4��6^   $��6��6o   6�   �� 5   
   -   5         ��Name$ audioSlider��5      $��3��6&   $��
0,27,0,-24q�6[   $��6��6o   �� 5   
   -   5         ��Name$ OpenBtn��5      $��Выбор Папки��6#   $��Center=�69   $��Center=�6V   $��29��6q   $	��148��6}   $��5��6�   �� 5   
   -   5        $
 Play��5      $��2��6    $��3��6-    *  &MaterialDesignFloatingActionMiniButton#���   6=   .��5      '  !MaterialDesignThemes.Wpf.PackIcon  -   5          Name$ Play1��5   &      Kind$
 Play=�5    !   $��39��5!   !   $��39��6-   
��� ���5"   !   5$      �� 5%   
   -   5        $
 Next��5%      $��2��6   $��4��6,   #���   6<   .��5&        $ SkipNext=�5'   !   $��39��5(   !   $��39��6-   
����  �	 � Cursor$
	 Hand��5)   3   5+      �� 5,   
   -   5        $
 Back��5,      $��2��6   $��2��6,   #���   6<   .��5-        $ SkipPrevious=�5.   !   $��40��5/   !   $��41��6-   
����  �
 J� RenderTransformOrigin$
 0.003,0.944&�50   3   51      54      �� 55   
   -   5        $ Shuffle��55      $��2��6"   $��5��6/   #���   6?   .��56        $ ShuffleVariant=�57   !   $��40��58   !   $��41��6-   
��� � �$
 0.003,0.944&�59   4   5:      �� 5;   
   -	   5        $ Repeat��5;      $��2��6!   $��1��6.   #���   6>   .��5<        $ Repeat=�5=   !   $��40��5>   !   $��41��6-   
��� � �$
 0.003,0.944&�5?   4   $��Center=�6X   5@      5C      5D      