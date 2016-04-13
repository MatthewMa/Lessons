package usis;

import java.io.Serializable;
import java.util.List;

public class Screens implements Serializable {
	
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	String text,type,video_url,audio_url,question;
	Integer id;
	
	List<Questions> choices;
	List<Images> images;
	


	public List<Questions> getChoices() {
		return choices;
	}

	public void setChoices(List<Questions> choices) {
		this.choices = choices;
	}

	public List<Images> getImages() {
		return images;
	}

	public void setImages(List<Images> images) {
		this.images = images;
	}

	public Integer getId() {
		return id;
	}

	public void setId(Integer id) {
		this.id = id;
	}

	public String getText() {
		return text;
	}

	public void setText(String text) {
		this.text = text;
	}

	public String getType() {
		return type;
	}

	public void setType(String type) {
		this.type = type;
	}

	public String getVideo_url() {
		return video_url;
	}

	public void setVideo_url(String video_url) {
		this.video_url = video_url;
	}

	public String getAudio_url() {
		return audio_url;
	}

	public void setAudio_url(String audio_url) {
		this.audio_url = audio_url;
	}

	public String getQuestion() {
		return question;
	}

	public void setQuestion(String question) {
		this.question = question;
	}
	
	
	
}
