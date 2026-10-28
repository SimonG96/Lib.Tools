// Author: Simon Gockner
// Created: 2022-11-11
// Copyright(c) 2022 SimonG. All Rights Reserved.

using System.Globalization;

namespace Lib.Tools;

public static class Dates
{
    public static int GetWeekOfYear(this DateTime date, CalendarWeekRule rule = CalendarWeekRule.FirstDay, DayOfWeek firstDayOfWeek = DayOfWeek.Monday)
    {
        Calendar currentCalendar = CultureInfo.CurrentCulture.Calendar;
        return currentCalendar.GetWeekOfYear(date, rule, firstDayOfWeek);
    }
    
    public static bool IsInSameWeek(this DateTime date, DateTime referenceDate)
    {
        int referenceWeek = referenceDate.GetWeekOfYear();
        int currentWeek = date.GetWeekOfYear();

        return referenceWeek == currentWeek;
    }

    public static bool EqualsDay(this DateTime to, DateTime compare) => to.Year == compare.Year && to.Month == compare.Month && to.Day == compare.Day;
    public static bool EqualsDay(this DateOnly to, DateOnly compare) => to.Year == compare.Year && to.Month == compare.Month && to.Day == compare.Day;
    public static bool EqualsDay(this DateTime to, DateOnly compare) => to.Year == compare.Year && to.Month == compare.Month && to.Day == compare.Day;
    public static bool EqualsDay(this DateOnly to, DateTime compare) => compare.EqualsDay(to);
    public static bool IsToday(this DateTime date) => DateTime.Today.EqualsDay(date);

    public static List<DateTime> GetDatesOfWeek(this DateTime date)
    {
        int dayOfWeek = (int) date.DayOfWeek - 1;
        if (dayOfWeek == -1) //Sunday is -1
            dayOfWeek = 6; //Start week on Monday

        List<DateTime> dates = [];
        for (int i = 0; i < 7; i++)
        {
            DateTime day = date.AddDays(i).Subtract(new TimeSpan(dayOfWeek, 0, 0, 0));
            dates.Add(day);
        }

        return dates;
    }
    
    public static DateOnly ToDateOnly(this DateTime dateTime) => DateOnly.FromDateTime(dateTime);
    public static DateTime GetPreviousMonth(this DateTime currentMonth) => currentMonth.Month - 1 > 0 ? 
        new DateTime(currentMonth.Year, currentMonth.Month - 1, 1) : new DateTime(currentMonth.Year - 1, 12, 1);

    public static DateTime GetNextMonth(this DateTime currentMonth) => currentMonth.Month + 1 <= 12 ?
        new DateTime(currentMonth.Year, currentMonth.Month + 1, 1) : new DateTime(currentMonth.Year + 1, 1, 1);
}