package usis;

import java.io.Serializable;

import to.LessonBr;

public class Lesson extends LessonBr implements Serializable{
	/**
	 * 
	 */
	private static final long serialVersionUID = 2671368814956684351L;
	
	String description;
	Integer screenCount;
	
	
	public String getDescription() {
		return description;
	}
	public void setDescription(String description) {
		this.description = description;
	}
	
	public Integer getScreenCount() {
		return screenCount;
	}
	public void setScreenCount(Integer screenCount) {
		this.screenCount = screenCount;
	}
	
}
