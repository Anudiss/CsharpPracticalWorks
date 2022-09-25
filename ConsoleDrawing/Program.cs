﻿using static ConsoleDrawing.IO.Console;
using ConsoleDrawing.Types;

namespace ConsoleDrawing
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras vulputate nec ipsum in laoreet. Suspendisse sapien dolor, gravida ac facilisis quis, facilisis sodales massa. Aenean lacinia, nisl eu ornare luctus, erat nisl varius ex, finibus vehicula arcu metus non leo. Mauris sit amet molestie metus. Nam odio nunc, feugiat eu eros sit amet, pellentesque malesuada ex. Nulla facilisi. Nullam non leo facilisis, egestas massa ut, ullamcorper arcu. In malesuada ullamcorper nibh, non porta dolor fringilla pharetra. Phasellus consectetur, felis fermentum sagittis venenatis, lacus magna tempor tortor, ut fermentum tellus enim et risus. Vestibulum varius lacus non velit bibendum porta. Nulla consectetur quis leo et blandit. Maecenas scelerisque dolor id nunc ultricies malesuada. Cras suscipit, nisl vehicula condimentum suscipit, mauris libero mattis neque, id lacinia metus lorem nec nulla.\r\n\r\nNullam tempor, purus ut consectetur cursus, neque metus consectetur lacus, id facilisis ex sapien fermentum velit. Nullam sed consequat tortor. Vestibulum eleifend leo sit amet libero lacinia, eu interdum metus tincidunt. In vel erat id lorem vehicula condimentum ut in augue. Duis vel mattis augue, eu lacinia mi. Sed blandit porttitor lacinia. Donec enim dolor, semper varius lobortis sit amet, malesuada in ipsum. Ut sapien augue, malesuada at semper non, porttitor eget nisi. Cras facilisis sagittis consectetur. Vestibulum ullamcorper sagittis ornare. Phasellus vehicula nibh sit amet efficitur faucibus. In velit dui, sodales non cursus non, sodales sit amet eros. Etiam in sapien et ligula ornare vestibulum nec finibus ligula. Vivamus ut dignissim metus, eu vestibulum nibh. Fusce euismod dictum ante, sed ultrices arcu bibendum sed. Fusce viverra, ante in interdum pellentesque, enim neque dapibus dui, eget laoreet nibh nisl a dui.\r\n\r\nCras eget porttitor est. In non feugiat purus. Pellentesque dignissim eros semper vulputate tincidunt. Vivamus semper nunc quis tellus sodales, in molestie mauris dignissim. Fusce tempor leo eu leo tincidunt aliquam. Proin et ultricies lorem. Fusce mattis nisi vel massa faucibus maximus. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.\r\n\r\nNulla nibh ipsum, volutpat non imperdiet sit amet, pharetra a mauris. Donec iaculis, neque non bibendum pharetra, turpis velit sollicitudin justo, eu elementum dolor quam at neque. Suspendisse tempus sodales orci, nec ultrices magna suscipit et. Etiam blandit, tortor nec euismod tempor, ipsum tellus ornare libero, porttitor tempor neque lacus nec turpis. Aenean odio erat, accumsan quis volutpat non, luctus sed ligula. Donec ac ex massa. Proin porta consequat condimentum. Ut nisl nulla, pellentesque et tristique eu, venenatis ac enim. Integer tincidunt facilisis lorem sit amet tincidunt. Nulla euismod volutpat nibh ac semper. Donec pulvinar sagittis magna, eget tempor justo. Nunc vel quam tempor, convallis urna eu, euismod augue. Ut molestie risus sed turpis ultrices, a rutrum nisi maximus. Duis ex turpis, elementum tempus consequat eget, laoreet a ligula. Suspendisse eget ornare metus.\r\n\r\nPraesent vitae varius leo, et sodales ipsum. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Quisque quis nisl orci. Integer consequat a augue non convallis. Suspendisse ultricies maximus leo, sed blandit eros ultrices sed. Duis libero augue, blandit eget nibh eget, eleifend semper nisi. Nam molestie tortor id leo euismod vestibulum. In a nulla quis ipsum semper aliquet volutpat vel nisl. Mauris quis faucibus dui. Aliquam erat volutpat. Quisque accumsan libero sit amet leo condimentum, et vehicula urna pretium. Suspendisse placerat euismod justo. In hac habitasse platea dictumst. Sed a condimentum neque.";
            Box box = new(0, 0, 57, 5);

            Write(text);
        }
    }

}