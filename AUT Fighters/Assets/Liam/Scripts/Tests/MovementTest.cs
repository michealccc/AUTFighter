using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class MovementTest
{
    [Test]
    public void Left_Movement_Test()
    {
        //Arrange
        var character = new GameObject().AddComponent<CharacterController>();
        character.gameObject.AddComponent<Rigidbody2D>();
        character.rb = character.gameObject.GetComponent<Rigidbody2D>();
        character.GetComponent<CharacterController>().moveSpeed = 225f;
        character.GetComponent<CharacterController>().moveDir = -1f;

        //Act
        float expectedOutput = character.GetComponent<CharacterController>().moveSpeed * character.GetComponent<CharacterController>().moveDir * Time.deltaTime;
        character.GetComponent<CharacterController>().Walk();
        float actualOutput = character.GetComponent<CharacterController>().rb.velocity.x;

        Assert.AreEqual(expectedOutput, actualOutput);
    }

    [Test]
    public void Right_Movement_Test()
    {
        //Arrange
        var character = new GameObject().AddComponent<CharacterController>();
        character.gameObject.AddComponent<Rigidbody2D>();
        character.rb = character.gameObject.GetComponent<Rigidbody2D>();
        character.GetComponent<CharacterController>().moveSpeed = 225f;
        character.GetComponent<CharacterController>().moveDir = 1f;

        //Act
        float expectedOutput = character.GetComponent<CharacterController>().moveSpeed * character.GetComponent<CharacterController>().moveDir * Time.deltaTime;
        character.GetComponent<CharacterController>().Walk();
        float actualOutput = character.GetComponent<CharacterController>().rb.velocity.x;

        Assert.AreEqual(expectedOutput, actualOutput);
    }

    [Test]
    public void Neutral_Jump_Test()
    {
        //Arrange
        var character = new GameObject().AddComponent<CharacterController>();
        character.gameObject.AddComponent<Rigidbody2D>();
        character.rb = character.gameObject.GetComponent<Rigidbody2D>();
        character.GetComponent<CharacterController>().jumpForceX = 125f;
        character.GetComponent<CharacterController>().jumpForceY = 12f;
        character.GetComponent<CharacterController>().moveDir = 0f;
        Vector2 expectedOutput;

        //Act
        expectedOutput.x = character.GetComponent<CharacterController>().jumpForceX * character.GetComponent<CharacterController>().moveDir * Time.deltaTime;
        expectedOutput.y = character.GetComponent<CharacterController>().jumpForceY;

        character.GetComponent<CharacterController>().Jump();
        Vector2 actualOutput = character.GetComponent<CharacterController>().rb.velocity;

        Assert.AreEqual(expectedOutput, actualOutput);
    }

    [Test]
    public void Forward_Jump_Test()
    {
        //Arrange
        var character = new GameObject().AddComponent<CharacterController>();
        character.gameObject.AddComponent<Rigidbody2D>();
        character.rb = character.gameObject.GetComponent<Rigidbody2D>();
        character.GetComponent<CharacterController>().jumpForceX = 125f;
        character.GetComponent<CharacterController>().jumpForceY = 12f;
        character.GetComponent<CharacterController>().moveDir = 1f;
        Vector2 expectedOutput;

        //Act
        expectedOutput.x = character.GetComponent<CharacterController>().jumpForceX * character.GetComponent<CharacterController>().moveDir * Time.deltaTime;
        expectedOutput.y = character.GetComponent<CharacterController>().jumpForceY;

        character.GetComponent<CharacterController>().Jump();
        Vector2 actualOutput = character.GetComponent<CharacterController>().rb.velocity;

        Assert.AreEqual(expectedOutput, actualOutput);
    }

    [Test]
    public void Back_Jump_Test()
    {
        //Arrange
        var character = new GameObject().AddComponent<CharacterController>();
        character.gameObject.AddComponent<Rigidbody2D>();
        character.rb = character.gameObject.GetComponent<Rigidbody2D>();
        character.GetComponent<CharacterController>().jumpForceX = 125f;
        character.GetComponent<CharacterController>().jumpForceY = 12f;
        character.GetComponent<CharacterController>().moveDir = -1f;
        Vector2 expectedOutput;

        //Act
        expectedOutput.x = character.GetComponent<CharacterController>().jumpForceX * character.GetComponent<CharacterController>().moveDir * Time.deltaTime;
        expectedOutput.y = character.GetComponent<CharacterController>().jumpForceY;

        character.GetComponent<CharacterController>().Jump();
        Vector2 actualOutput = character.GetComponent<CharacterController>().rb.velocity;

        Assert.AreEqual(expectedOutput, actualOutput);
    }

    [Test]
    public void Crouch_Test()
    {
        //Arrange
        var character = new GameObject().AddComponent<CharacterController>();
        character.gameObject.AddComponent<Rigidbody2D>();
        bool expectedOutput = true;

        //Act
        bool actualOutput = character.isCrouching;

        Assert.AreEqual(expectedOutput, actualOutput);
    }
}
