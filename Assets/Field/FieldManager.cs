using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{

    private Field field;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void generateField(Field field)
    {
        this.field = field;
        renameObject();
    }

    private void renameObject()
    {
        gameObject.name = field.ToString();
    }
}