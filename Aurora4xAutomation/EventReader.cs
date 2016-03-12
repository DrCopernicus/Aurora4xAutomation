using System.Drawing;
namespace Aurora4xAutomation
{
    public static class EventReader
    {
        private static int XPos = 1253;

        public static int GetStopCode(this Image screen)
        {
            var currentStopCode = 0;

            var readingEventsHasAlmostStarted = false;
            var readingEventsHasStarted = false;
            var eventIsAlreadyRead = false;

            using (var bit = new Bitmap(screen))
            {
                for (int i = screen.Height - 1; i > 0; i--)
                {
                    var pixel = bit.GetPixel(XPos, i);

                    if (!readingEventsHasAlmostStarted)
                    {
                        if (IsEmptySpaceBelowEvents(pixel))
                            readingEventsHasAlmostStarted = true;
                        continue;
                    }

                    if (!readingEventsHasStarted)
                    {
                        if (IsBlackBarBelowLastEvent(pixel))
                            readingEventsHasStarted = true;
                        continue;
                    }

                    if (IsEndOfEventsLine(pixel))
                        return currentStopCode;

                    if (!eventIsAlreadyRead)
                    {
                        if (IsTableLine(pixel))
                            continue;

                        if (IsNewCycleEvent(pixel))
                            return currentStopCode;

                        if (IsResearchEvent(pixel)
                            || IsInactiveLabEvent(pixel))
                            return 1;

                        if (IsCivilianConstructionEvent(pixel)
                            || IsCivilianMineEvent(pixel)
                            || IsShipConstructionEvent(pixel))
                            currentStopCode = 2;

                        eventIsAlreadyRead = true;
                    }
                    else
                    {
                        if (IsTableLine(pixel))
                        {
                            eventIsAlreadyRead = false;
                        }
                    }
                }
            }

            return currentStopCode;
        }

        private static bool IsResearchEvent(Color pixel)
        {
            return pixel.IsColor(0, 0, 255);
        }

        private static bool IsInactiveLabEvent(Color pixel)
        {
            return pixel.IsColor(255, 0, 128);
        }

        private static bool IsShipConstructionEvent(Color pixel)
        {
            return pixel.IsColor(255, 0, 0);
        }

        private static bool IsCivilianConstructionEvent(Color pixel)
        {
            return pixel.IsColor(66, 210, 189);
        }

        private static bool IsCivilianMineEvent(Color pixel)
        {
            return pixel.IsColor(255, 255, 255);
        }

        private static bool IsTableLine(Color pixel)
        {
            return pixel.IsColor(192, 192, 192);
        }

        private static bool IsEmptySpaceBelowEvents(Color pixel)
        {
            return pixel.IsColor(128, 128, 128);
        }

        private static bool IsBlackBarBelowLastEvent(Color pixel)
        {
            return pixel.IsColor(0, 0, 0);
        }

        private static bool IsEndOfEventsLine(Color pixel)
        {
            return pixel.IsColor(105, 105, 105);
        }

        private static bool IsNewCycleEvent(Color pixel)
        {
            return pixel.IsColor(220, 220, 220);
        }

        private static bool IsColor(this Color pixel, byte r, byte g, byte b)
        {
            return pixel.R == r && pixel.G == g && pixel.B == b;
        }

    }
}
