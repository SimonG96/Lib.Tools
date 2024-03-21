// Author: Simon Gockner
// Created: 2022-11-11
// Copyright(c) 2022 SimonG. All Rights Reserved.

using System.Globalization;

namespace Lib.Tools;

public static class Dates
{
    public static bool IsInSameWeek(this DateTime date, DateTime referenceDate)
    {
        Calendar currentCalendar = CultureInfo.CurrentCulture.Calendar;
        int referenceWeek = currentCalendar.GetWeekOfYear(referenceDate, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        int currentWeek = currentCalendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

        return referenceWeek == currentWeek;
    }

    public static bool EqualsDay(this DateTime to, DateTime compare) => to.Year == compare.Year && to.Month == compare.Month && to.Day == compare.Day;
    public static bool IsToday(this DateTime date) => DateTime.Today.EqualsDay(date);

    public static List<DateTime> GetDatesOfWeek(this DateTime date)
    {
        int dayOfWeek = (int) date.DayOfWeek - 1;
        if (dayOfWeek == -1) //Sunday is -1
            dayOfWeek = 6; //Start week on Monday

        List<DateTime> dates = new();
        for (int i = 0; i < 7; i++)
        {
            DateTime day = date.AddDays(i).Subtract(new TimeSpan(dayOfWeek, 0, 0, 0));
            dates.Add(day);
        }

        return dates;
    }
}