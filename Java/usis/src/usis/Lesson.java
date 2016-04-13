package usis;

import java.io.Serializable;

public class Lesson implements Serializable{
	/**
	 * 
	 */
	private static final long serialVersionUID = 2671368814956684351L;
	
	String title,description;
	Integer id,screenCount;
	public String getTitle() {
		return title;
	}
	public void setTitle(String title) {
		this.title = title;
	}
	public String getDescription() {
		return description;
	}
	public void setDescription(String description) {
		this.description = description;
	}
	public Integer getId() {
		return id;
	}
	public void setId(Integer id) {
		this.id = id;
	}
	public Integer getScreenCount() {
		return screenCount;
	}
	public void setScreenCount(Integer screenCount) {
		this.screenCount = screenCount;
	}
	
}
