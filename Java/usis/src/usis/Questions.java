package usis;

import java.io.Serializable;

public class Questions implements Serializable{
	
	/**
	 * 
	 */
	private static final long serialVersionUID = 9004299811466215023L;

	public String getTitle() {
		return title;
	}

	public void setTitle(String title) {
		this.title = title;
	}

	public String getDetail() {
		return detail;
	}

	public void setDetail(String detail) {
		this.detail = detail;
	}

	String title,detail;
	
	
}
