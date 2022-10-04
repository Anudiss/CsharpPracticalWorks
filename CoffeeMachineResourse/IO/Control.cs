using System.Linq;
using System;
using System.Collections.Generic;

namespace CoffeeMachineResourse.IO
{
    public static class Control
    {
        public static Dictionary<ControlCommand, KeyInfo> ControlKeys = new Dictionary<ControlCommand, KeyInfo>
        {
            { ControlCommand.Increment, new KeyInfo() { Key = ConsoleKey.OemPlus } },
            { ControlCommand.HugeIncrement, new KeyInfo() { Key = ConsoleKey.OemPlus, Modifiers = ConsoleModifiers.Shift } },
            { ControlCommand.Decrement, new KeyInfo() { Key = ConsoleKey.OemMinus } },
            { ControlCommand.HugeDecrement, new KeyInfo() { Key = ConsoleKey.OemMinus, Modifiers = ConsoleModifiers.Shift } },
            { ControlCommand.MoveLeft, new KeyInfo() { Key = ConsoleKey.LeftArrow } },
            { ControlCommand.MoveRigt, new KeyInfo() { Key = ConsoleKey.RightArrow } },
            { ControlCommand.MoveUp, new KeyInfo() { Key = ConsoleKey.UpArrow } },
            { ControlCommand.MoveDown, new KeyInfo() { Key = ConsoleKey.DownArrow } },
            { ControlCommand.Select, new KeyInfo() { Key = ConsoleKey.Enter } },
            { ControlCommand.FocusNext, new KeyInfo() { Key = ConsoleKey.Tab } },
            { ControlCommand.FocusPrevious, new KeyInfo() { Key = ConsoleKey.Tab, Modifiers = ConsoleModifiers.Shift } },
            { ControlCommand.Preparing, new KeyInfo() { Key = ConsoleKey.Enter, Modifiers = ConsoleModifiers.Shift } },
            { ControlCommand.Delete, new KeyInfo() { Key = ConsoleKey.Delete } }
        };

        public static string Print(ControlCommand controlCommand, KeyInfo keyInfo)
        {
            //←↑→↓
            int max = (Enum.GetValues(typeof(ControlCommand)) as ControlCommand[]).Max(e => $"{e}".Length) + 2;
            string result = $"{controlCommand} ".PadRight(max);
            if (keyInfo.Modifiers.HasFlag(ConsoleModifiers.Shift))
                result += "Shift + ";

            if (keyInfo.Key == ConsoleKey.OemPlus)
                result += "+";
            else if (keyInfo.Key == ConsoleKey.OemMinus)
                result += "-";
            else if (keyInfo.Key == ConsoleKey.LeftArrow)
                result += "←";
            else if (keyInfo.Key == ConsoleKey.RightArrow)
                result += "→";
            else if (keyInfo.Key == ConsoleKey.UpArrow)
                result += "↑";
            else if (keyInfo.Key == ConsoleKey.DownArrow)
                result += "↓";
            else if (keyInfo.Key == ConsoleKey.Tab)
                result += "Tab";
            else if (keyInfo.Key == ConsoleKey.Enter)
                result += "↲";
            else if (keyInfo.Key == ConsoleKey.Delete)
                result += "DEL";

            return result;
        }
    }

    public enum ControlCommand
    {
        Increment,
        Decrement,
        HugeIncrement,
        HugeDecrement,
        MoveLeft,
        MoveRigt,
        MoveUp,
        MoveDown,
        Select,
        FocusNext,
        FocusPrevious,
        Delete,
        Preparing
    }

    public struct KeyInfo
    {
        public ConsoleKey Key { get; set; }
        public ConsoleModifiers Modifiers { get; set; }

        public KeyInfo(ConsoleKey key, ConsoleModifiers modifiers)
        {
            Key = key;
            Modifiers = modifiers;
        }

        public static bool operator ==(KeyInfo keyInfo1, KeyInfo keyInfo2) => keyInfo1.Key == keyInfo2.Key && keyInfo1.Modifiers == keyInfo2.Modifiers;
        public static bool operator !=(KeyInfo keyInfo1, KeyInfo keyInfo2) => keyInfo1.Key != keyInfo2.Key || keyInfo1.Modifiers != keyInfo2.Modifiers;

        public override bool Equals(object obj)
        {
            return obj is KeyInfo info &&
                   Key == info.Key &&
                   Modifiers == info.Modifiers;
        }
        public override int GetHashCode()
        {
            int hashCode = 34518437;
            hashCode = hashCode * -1521134295 + Key.GetHashCode();
            hashCode = hashCode * -1521134295 + Modifiers.GetHashCode();
            return hashCode;
        }
    }

}
