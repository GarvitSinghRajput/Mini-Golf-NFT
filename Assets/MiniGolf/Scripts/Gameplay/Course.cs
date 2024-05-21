using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

//The headers above many of the variables and the class are there
//for when we serialize this Course class.

[XmlRoot("Course")]
public class Course 
{
	[XmlAttribute("name")]
	public string courseName;

	public int courseDifficulty; //1 = easy, 2 = medium, 3 = hard

	[XmlArray("Holes")]
	[XmlArrayItem("Hole")]
	public CourseHole[] holes; 
}
