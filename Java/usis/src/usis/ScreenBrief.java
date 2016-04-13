package usis;

import java.io.Serializable;

public class ScreenBrief implements Serializable {
	/**
	 * 
	 */
	private static final long serialVersionUID = -3691673691123326264L;
	Integer id;
	String type;
	public Integer getId() {
		return id;
	}
	public void setId(Integer id) {
		this.id = id;
	}
	public String getType() {
		return type;
	}
	public void setType(String type) {
		this.type = type;
	}
}
