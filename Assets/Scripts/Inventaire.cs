using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventaire
{

    public Inventaire(int oeufs, int graines, int or)
    {
        NbOeufs = oeufs;
        NbOr = or;
        NbGraines = graines;
        NbChoux = 0;
    }

    public int NbOeufs
    {
        get;
        set;
    }

    public int NbGraines
    {
        get;
        set;
    }

    public int NbOr
    {
        get;
        set;
    }

    public int NbChoux
    {
        get; 
        set;
    }

    public int NbPoules
    {
        get;
        set;
    }

    public void Reinitialiser()
    {
        NbOeufs = 0;
        NbGraines = 0;
        NbOr = 0;
        NbChoux = 0;
        NbPoules = 0;
    }
}
