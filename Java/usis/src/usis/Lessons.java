package usis;

import java.io.Serializable;
import java.util.List;

public class Lessons implements Serializable{

	/**
	 * 
	 */
	private static final long serialVersionUID = 3365448570897691629L;
	
	String title,description;
	Integer id,screenCount;
	
	List<Screens> screens; 
	
	public List<Screens> getScreens() {
		return screens;
	}
	public void setScreens(List<Screens> screens) {
		this.screens = screens;
	}
	public Integer getId() {
		return id;
	}
	public void setId(Integer id) {
		this.id = id;
	}
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
	public Integer getScreenCount() {
		return screenCount;
	}
	public void setScreenCount(Integer screenCount) {
		this.screenCount = screenCount;
	}

	
}
