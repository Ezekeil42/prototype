using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Wish
{
    private string date;
    private int id;
    private string description;
    private int status;
    private int votes;

    public Wish(string date, int id, string description, int status, int votes = 0)
    {
        this.Date = date;
        this.Id = id;
        this.Description = description;
        this.Status = status;
        this.Votes = votes;
    }

    public string Date
    {
        get
        {
            return this.date;
        }
        set
        {
            this.date = value;
        }
    }

    public int Id
    {
        get
        {
            return this.id;
        }
        set
        {
            this.id = value;
        }
    }

    public string Description
    {
        get
        {
            return this.description;
        }
        set
        {
            this.description = value;
        }
    }

    public int Status
    {
        get
        {
            return this.status;
        }
        set
        {
            this.status = value;
        }
    }

    public int Votes
    {
        get
        {
            return this.votes;
        }
        set
        {
            this.votes = value;
        }
    }
}