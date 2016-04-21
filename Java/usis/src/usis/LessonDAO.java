package usis;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

import settings.Settings;
import to.ImageTO;
import to.LessonBr;
import to.LessonScreenTO;
import to.QuestionTO;
import to.ScreenComplete;
import to.ScreenTO;

public class LessonDAO {

	public static List<LessonBr> getBriefLessons() {

		List<LessonBr> lessonList = new ArrayList<LessonBr>();
		try {
			Connection connection = Util.getConnection();
			Statement statement = connection.createStatement();
			ResultSet resultSet = statement.executeQuery("select id,title,screencount,description from lessons order by id");
			while (resultSet.next()) {
				LessonBr lesson = new LessonBr();
				lesson.setId(resultSet.getInt("id"));
				lesson.setTitle(resultSet.getString("title"));
				lesson.setUrl(Settings.BASE_URL+""+resultSet.getInt("id"));
				lesson.setScreenCount(resultSet.getInt("screencount"));
				lesson.setDescription(resultSet.getString("description"));
				
				lessonList.add(lesson);
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		return lessonList;
	}
	
	public static LessonBr getLessonBriefLessonById(String id) {
		LessonBr lesson= new  LessonBr();
		try {
			Connection connection = Util.getConnection();
			Statement screenStatement = connection.createStatement();
			String lessonSQL = "select * from lessons where id=" +id+ "";
			ResultSet lessonResultset = screenStatement.executeQuery(lessonSQL);
			
			while (lessonResultset.next()) {
				lesson.setId(lessonResultset.getInt("id"));
				lesson.setTitle(lessonResultset.getString("title"));
				lesson.setUrl(Settings.BASE_URL+""+lessonResultset.getInt("id"));
				lesson.setScreenCount(lessonResultset.getInt("screencount"));
			}
			Util.closeConnection(connection, screenStatement, lessonResultset);
		} catch (Exception ee) {
			ee.printStackTrace();
		}
		return lesson;
	}
	
	public static List<Screen> getScreensLessonById(String id) {
		List<Screen> screens = new ArrayList<Screen>();
		try {
			Connection connection = Util.getConnection();
			Statement screenStatement = connection.createStatement();
			String screenSQL = "select * from screens where lesson_id=" +id+ " order by lesson_id,s_order";
			ResultSet screenResultSet = screenStatement.executeQuery(screenSQL);
			int position=0;
			while (screenResultSet.next()) {
				Screen screen = new Screen();
				screen.setId(screenResultSet.getInt("id"));
				screen.setText(screenResultSet.getString("text"));
				screen.setType(screenResultSet.getString("type"));
				screen.setVideo_url(screenResultSet.getString("video_url"));
				screen.setAudio_url(screenResultSet.getString("audio_url"));
				screen.setQuestion(screenResultSet.getString("question"));
				screen.setPosition(position);
				screen.setImagesUrl(Settings.BASE_URL+""+id+"/screens/"+screenResultSet.getInt("id")+"/images");
				screen.setOptionsUrl(Settings.BASE_URL+id+"/screens/"+screenResultSet.getInt("id")+"/options");
				screens.add(screen);
				position+=1;
			}
			Util.closeConnection(connection, screenStatement, screenResultSet);
		} catch (Exception ee) {
			ee.printStackTrace();
		}
		return screens;
	}
	
	public static ScreenComplete getCompleteScreensById(String id) {
		ScreenComplete screen =new ScreenComplete();
		try {
			Connection connection = Util.getConnection();
			Statement screenStatement = connection.createStatement();
			String screenSQL = "select * from screens where id=" +id+ " order by lesson_id,s_order";
			ResultSet screenResultSet = screenStatement.executeQuery(screenSQL);
			
			while (screenResultSet.next()) {
				screen.setId(screenResultSet.getInt("id"));
				screen.setText(screenResultSet.getString("text"));
				screen.setType(screenResultSet.getString("type"));
				screen.setVideo_url(screenResultSet.getString("video_url"));
				screen.setAudio_url(screenResultSet.getString("audio_url"));
				screen.setQuestion(screenResultSet.getString("question"));
				screen.setPosition(screenResultSet.getInt("s_order")-1);
				
				List<ImageTO> imageList = new ArrayList<ImageTO>();
				Statement imagesStatement = connection.createStatement();
				String imageSQL = "select * from images where screen_id=" + screenResultSet.getInt("id")
						+ " order by id";
				// System.out.println("imageSQL:"+imageSQL);
				ResultSet imagesResultset = imagesStatement.executeQuery(imageSQL);
				while (imagesResultset.next()) {
					ImageTO image = new ImageTO();
					image.setTitle(imagesResultset.getString("title"));
					image.setUrl(imagesResultset.getString("url"));
					imageList.add(image);
				}
				Util.closeConnection(null, imagesStatement, imagesResultset);

				List<QuestionTO> questionList = new ArrayList<QuestionTO>();
				Statement questionStatement = connection.createStatement();
				String questionSQL = "select * from questions where screen_id=" + screenResultSet.getInt("id")
						+ " order by q_order ";
				// System.out.println("questionSQL:"+questionSQL);
				ResultSet questionResultset = questionStatement.executeQuery(questionSQL);
				while (questionResultset.next()) {
					QuestionTO question = new QuestionTO();
					question.setTitle(questionResultset.getString("title"));
					question.setDetail(questionResultset.getString("detail"));
					question.setOrder(questionResultset.getInt("q_order"));
					questionList.add(question);
				}

				Util.closeConnection(null, questionStatement, questionResultset);

				screen.setOptions(questionList);
				screen.setImages(imageList);
				
				
			}
			Util.closeConnection(connection, screenStatement, screenResultSet);
		} catch (Exception ee) {
			ee.printStackTrace();
		}
		return screen;
	}
	
	
	public static ScreenComplete getScreenByPosition(String lessonId,Integer position) {
		ScreenComplete screen =new ScreenComplete();
		try {
			Connection connection = Util.getConnection();
			Statement screenStatement = connection.createStatement();
			String screenSQL = "select * from screens where lesson_id=" +lessonId+" and s_order="+(position+1)+ " order by lesson_id,s_order";
			ResultSet screenResultSet = screenStatement.executeQuery(screenSQL);
			
			while (screenResultSet.next()) {
				screen.setId(screenResultSet.getInt("id"));
				screen.setText(screenResultSet.getString("text"));
				screen.setType(screenResultSet.getString("type"));
				screen.setVideo_url(screenResultSet.getString("video_url"));
				screen.setAudio_url(screenResultSet.getString("audio_url"));
				screen.setQuestion(screenResultSet.getString("question"));
				screen.setPosition(screenResultSet.getInt("s_order")-1);
				List<ImageTO> imageList = new ArrayList<ImageTO>();
				Statement imagesStatement = connection.createStatement();
				String imageSQL = "select * from images where screen_id=" + screenResultSet.getInt("id")
						+ " order by id";
				// System.out.println("imageSQL:"+imageSQL);
				ResultSet imagesResultset = imagesStatement.executeQuery(imageSQL);
				while (imagesResultset.next()) {
					ImageTO image = new ImageTO();
					image.setTitle(imagesResultset.getString("title"));
					image.setUrl(imagesResultset.getString("url"));
					imageList.add(image);
				}
				Util.closeConnection(null, imagesStatement, imagesResultset);

				List<QuestionTO> questionList = new ArrayList<QuestionTO>();
				Statement questionStatement = connection.createStatement();
				String questionSQL = "select * from questions where screen_id=" + screenResultSet.getInt("id")
						+ " order by q_order ";
				// System.out.println("questionSQL:"+questionSQL);
				ResultSet questionResultset = questionStatement.executeQuery(questionSQL);
				while (questionResultset.next()) {
					QuestionTO question = new QuestionTO();
					question.setTitle(questionResultset.getString("title"));
					question.setDetail(questionResultset.getString("detail"));
					question.setOrder(questionResultset.getInt("q_order"));
					questionList.add(question);
				}
				screen.setImages(imageList);
				screen.setOptions(questionList);
			}
			Util.closeConnection(connection, screenStatement, screenResultSet);
		} catch (Exception ee) {
			ee.printStackTrace();
		}
		return screen;
	}
	
	public static ScreenComplete getNextScreenByCurrentScreen(Integer screenId) {
		ScreenComplete screen =new ScreenComplete();
		try {
			Connection connection = Util.getConnection();
			Statement screenStatement = connection.createStatement();
			String screenSQL = "select * from screens where s_order= (select s_order+1 from screens where id= "+screenId
					+ ") and lesson_id = (select lesson_id from screens where id="+screenId+")";
			System.out.println("screen SQL"+screenSQL);
			ResultSet screenResultSet = screenStatement.executeQuery(screenSQL);
			
			while (screenResultSet.next()) {
				screen.setId(screenResultSet.getInt("id"));
				screen.setText(screenResultSet.getString("text"));
				screen.setType(screenResultSet.getString("type"));
				screen.setVideo_url(screenResultSet.getString("video_url"));
				screen.setAudio_url(screenResultSet.getString("audio_url"));
				screen.setQuestion(screenResultSet.getString("question"));
				screen.setPosition(screenResultSet.getInt("s_order")-1);
				List<ImageTO> imageList = new ArrayList<ImageTO>();
				Statement imagesStatement = connection.createStatement();
				String imageSQL = "select * from images where screen_id=" + screenResultSet.getInt("id")
						+ " order by id";
				// System.out.println("imageSQL:"+imageSQL);
				ResultSet imagesResultset = imagesStatement.executeQuery(imageSQL);
				while (imagesResultset.next()) {
					ImageTO image = new ImageTO();
					image.setTitle(imagesResultset.getString("title"));
					image.setUrl(imagesResultset.getString("url"));
					imageList.add(image);
				}
				Util.closeConnection(null, imagesStatement, imagesResultset);

				List<QuestionTO> questionList = new ArrayList<QuestionTO>();
				Statement questionStatement = connection.createStatement();
				String questionSQL = "select * from questions where screen_id=" + screenResultSet.getInt("id")
						+ " order by q_order ";
				// System.out.println("questionSQL:"+questionSQL);
				ResultSet questionResultset = questionStatement.executeQuery(questionSQL);
				while (questionResultset.next()) {
					QuestionTO question = new QuestionTO();
					question.setTitle(questionResultset.getString("title"));
					question.setDetail(questionResultset.getString("detail"));
					question.setOrder(questionResultset.getInt("q_order"));
					questionList.add(question);
				}
				screen.setImages(imageList);
				screen.setOptions(questionList);
			}
			Util.closeConnection(connection, screenStatement, screenResultSet);
		} catch (Exception ee) {
			ee.printStackTrace();
		}
		return screen;
	}
	
	
	
	
	
	
	
	
	public static Screen getScreensById(String id) {
		Screen screen =new Screen();
		try {
			Connection connection = Util.getConnection();
			Statement screenStatement = connection.createStatement();
			String screenSQL = "select * from screens where id=" +id+ " order by lesson_id,s_order";
			ResultSet screenResultSet = screenStatement.executeQuery(screenSQL);
			
			while (screenResultSet.next()) {
				screen.setId(screenResultSet.getInt("id"));
				screen.setText(screenResultSet.getString("text"));
				screen.setType(screenResultSet.getString("type"));
				screen.setVideo_url(screenResultSet.getString("video_url"));
				screen.setAudio_url(screenResultSet.getString("audio_url"));
				screen.setQuestion(screenResultSet.getString("question"));
				screen.setPosition(screenResultSet.getInt("s_order")-1);
				screen.setImagesUrl(Settings.BASE_URL+"lessons/"+screenResultSet.getInt("lesson_id")+"/screens/"+screenResultSet.getInt("id")+"/images");
				screen.setOptionsUrl(Settings.BASE_URL+"lessons/"+screenResultSet.getInt("lesson_id")+"/screens/"+screenResultSet.getInt("id")+"/options");
			}
			Util.closeConnection(connection, screenStatement, screenResultSet);
		} catch (Exception ee) {
			ee.printStackTrace();
		}
		return screen;
	}
	
	public static List<Lessons> getLessonList() {
		
		List<Lessons> lessonList = new ArrayList<Lessons>();
		try {
			Connection connection = Util.getConnection();
			Statement statement = connection.createStatement();
			ResultSet resultSet = statement.executeQuery("select * from lessons order by id");
			while (resultSet.next()) {
				Lessons lesson = new Lessons();
				lesson.setId(resultSet.getInt("id"));
				lesson.setTitle(resultSet.getString("title"));
				lesson.setDescription(resultSet.getString("description"));
				lesson.setScreenCount(resultSet.getInt("screencount"));

				Statement screenStatement = connection.createStatement();
				String screenSQL = "select * from screens where lesson_id=" + resultSet.getInt("id")
						+ " order by lesson_id,s_order";
				// System.out.println("screenSQL:"+screenSQL);
				ResultSet screenResultSet = screenStatement.executeQuery(screenSQL);
				List<Screens> screenList = new ArrayList<Screens>();
				while (screenResultSet.next()) {
					Screens screen = new Screens();
					screen.setId(screenResultSet.getInt("id"));
					screen.setText(screenResultSet.getString("text"));
					screen.setType(screenResultSet.getString("type"));
					screen.setVideo_url(screenResultSet.getString("video_url"));
					screen.setAudio_url(screenResultSet.getString("audio_url"));
					screen.setQuestion(screenResultSet.getString("question"));

					List<Images> imageList = new ArrayList<Images>();
					Statement imagesStatement = connection.createStatement();
					String imageSQL = "select * from images where screen_id=" + screenResultSet.getInt("id")
							+ " order by id";
					// System.out.println("imageSQL:"+imageSQL);
					ResultSet imagesResultset = imagesStatement.executeQuery(imageSQL);
					while (imagesResultset.next()) {
						Images image = new Images();
						image.setTitle(imagesResultset.getString("title"));
						image.setUrl(imagesResultset.getString("url"));
						imageList.add(image);
					}
					Util.closeConnection(null, imagesStatement, imagesResultset);

					List<Questions> questionList = new ArrayList<Questions>();
					Statement questionStatement = connection.createStatement();
					String questionSQL = "select * from questions where screen_id=" + screenResultSet.getInt("id")
							+ " order by q_order ";
					// System.out.println("questionSQL:"+questionSQL);
					ResultSet questionResultset = questionStatement.executeQuery(questionSQL);
					while (questionResultset.next()) {
						Questions question = new Questions();
						question.setTitle(questionResultset.getString("title"));
						question.setDetail(questionResultset.getString("detail"));
						questionList.add(question);
					}

					Util.closeConnection(null, questionStatement, questionResultset);

					screen.setChoices(questionList);
					screen.setImages(imageList);
					screenList.add(screen);
				}
				Util.closeConnection(null, screenStatement, screenResultSet);
				lesson.setScreens(screenList);

				lessonList.add(lesson);
			}
			Util.closeConnection(connection, statement, resultSet);

		} catch (Exception e) {
			e.printStackTrace();
		}
		// System.out.println("Lessonlist:"+lessonList.size());
		return lessonList;

	}

	public static List<ScreenBrief> getScreenBriefLessonById(String id) {
		List<ScreenBrief> screens = new ArrayList<ScreenBrief>();
		try {
			Connection connection = Util.getConnection();
			Statement screenStatement = connection.createStatement();
			String screenSQL = "select * from screens where lesson_id=" +id+ " order by lesson_id,s_order";
			ResultSet screenResultSet = screenStatement.executeQuery(screenSQL);
			
			while (screenResultSet.next()) {
				ScreenBrief screen = new ScreenBrief();
				screen.setId(screenResultSet.getInt("id"));
				screen.setType(screenResultSet.getString("type"));
				screens.add(screen);
			}
			Util.closeConnection(connection, screenStatement, screenResultSet);
		} catch (Exception ee) {
			ee.printStackTrace();
		}
		return screens;
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	public static LessonScreenTO getLessonWihScreenById(String id) {

		LessonScreenTO lesson = new LessonScreenTO();
		try {
			Connection connection = Util.getConnection();
			Statement statement = connection.createStatement();
			ResultSet resultSet = statement.executeQuery("select * from lessons where id=" + id + " order by id");

			while (resultSet.next()) {

				lesson.setId(resultSet.getInt("id"));
				lesson.setTitle(resultSet.getString("title"));
				lesson.setDescription(resultSet.getString("description"));
				lesson.setScreenCount(resultSet.getInt("screencount"));

				Statement screenStatement = connection.createStatement();
				String screenSQL = "select * from screens where lesson_id=" + resultSet.getInt("id")
						+ " order by lesson_id,s_order";
				// System.out.println("screenSQL:"+screenSQL);
				ResultSet screenResultSet = screenStatement.executeQuery(screenSQL);
				List<String> screenList = new ArrayList<String>();
				while (screenResultSet.next()) {
					screenList.add(Settings.BASE_URL+id+"/screens/"+""+screenResultSet.getInt("id"));
				}
				Util.closeConnection(null, screenStatement, screenResultSet);
				lesson.setScreenList(screenList);

			}
			Util.closeConnection(connection, statement, resultSet);

		} catch (Exception e) {
			e.printStackTrace();
		}

		return lesson;
	}

	public static Lessons getLessonById(String id) {

		Lessons lesson = new Lessons();
		try {
			Connection connection = Util.getConnection();
			Statement statement = connection.createStatement();
			ResultSet resultSet = statement.executeQuery("select * from lessons where id=" + id + " order by id");

			while (resultSet.next()) {

				lesson.setId(resultSet.getInt("id"));
				lesson.setTitle(resultSet.getString("title"));
				lesson.setDescription(resultSet.getString("description"));
				lesson.setScreenCount(resultSet.getInt("screencount"));

				Statement screenStatement = connection.createStatement();
				String screenSQL = "select * from screens where lesson_id=" + resultSet.getInt("id")
						+ " order by lesson_id,s_order";
				// System.out.println("screenSQL:"+screenSQL);
				ResultSet screenResultSet = screenStatement.executeQuery(screenSQL);
				List<Screens> screenList = new ArrayList<Screens>();
				while (screenResultSet.next()) {
					Screens screen = new Screens();
					screen.setId(screenResultSet.getInt("id"));
					screen.setText(screenResultSet.getString("text"));
					screen.setType(screenResultSet.getString("type"));
					screen.setVideo_url(screenResultSet.getString("video_url"));
					screen.setAudio_url(screenResultSet.getString("audio_url"));
					screen.setQuestion(screenResultSet.getString("question"));

					List<Images> imageList = new ArrayList<Images>();
					Statement imagesStatement = connection.createStatement();
					String imageSQL = "select * from images where screen_id=" + screenResultSet.getInt("id")
							+ " order by id";
					// System.out.println("imageSQL:"+imageSQL);
					ResultSet imagesResultset = imagesStatement.executeQuery(imageSQL);
					while (imagesResultset.next()) {
						Images image = new Images();
						image.setTitle(imagesResultset.getString("title"));
						image.setUrl(imagesResultset.getString("url"));
						imageList.add(image);
					}
					Util.closeConnection(null, imagesStatement, imagesResultset);

					List<Questions> questionList = new ArrayList<Questions>();
					Statement questionStatement = connection.createStatement();
					String questionSQL = "select * from questions where screen_id=" + screenResultSet.getInt("id")
							+ " order by q_order ";
					// System.out.println("questionSQL:"+questionSQL);
					ResultSet questionResultset = questionStatement.executeQuery(questionSQL);
					while (questionResultset.next()) {
						Questions question = new Questions();
						question.setTitle(questionResultset.getString("title"));
						question.setDetail(questionResultset.getString("detail"));
						questionList.add(question);
					}

					Util.closeConnection(null, questionStatement, questionResultset);

					screen.setChoices(questionList);
					screen.setImages(imageList);
					screenList.add(screen);
				}
				Util.closeConnection(null, screenStatement, screenResultSet);
				lesson.setScreens(screenList);

			}
			Util.closeConnection(connection, statement, resultSet);

		} catch (Exception e) {
			e.printStackTrace();
		}

		return lesson;
	}
	
	
	
	public static Screens getOneScreenById(String id) {

		Screens screen = new Screens();
		try {
				Connection connection = Util.getConnection();
				Statement screenStatement = connection.createStatement();
				String screenSQL = "select * from screens where id=" +id+ " order by lesson_id,s_order";
				// System.out.println("screenSQL:"+screenSQL);
				ResultSet screenResultSet = screenStatement.executeQuery(screenSQL);
			
				while (screenResultSet.next()) {
					screen.setId(screenResultSet.getInt("id"));
					screen.setText(screenResultSet.getString("text"));
					screen.setType(screenResultSet.getString("type"));
					screen.setVideo_url(screenResultSet.getString("video_url"));
					screen.setAudio_url(screenResultSet.getString("audio_url"));
					screen.setQuestion(screenResultSet.getString("question"));

					List<Images> imageList = new ArrayList<Images>();
					Statement imagesStatement = connection.createStatement();
					String imageSQL = "select * from images where screen_id=" + screenResultSet.getInt("id")
							+ " order by id";
					// System.out.println("imageSQL:"+imageSQL);
					ResultSet imagesResultset = imagesStatement.executeQuery(imageSQL);
					while (imagesResultset.next()) {
						Images image = new Images();
						image.setTitle(imagesResultset.getString("title"));
						image.setUrl(imagesResultset.getString("url"));
						imageList.add(image);
					}
					Util.closeConnection(null, imagesStatement, imagesResultset);

					List<Questions> questionList = new ArrayList<Questions>();
					Statement questionStatement = connection.createStatement();
					String questionSQL = "select * from questions where screen_id=" + screenResultSet.getInt("id")
							+ " order by q_order ";
					// System.out.println("questionSQL:"+questionSQL);
					ResultSet questionResultset = questionStatement.executeQuery(questionSQL);
					while (questionResultset.next()) {
						Questions question = new Questions();
						question.setTitle(questionResultset.getString("title"));
						question.setDetail(questionResultset.getString("detail"));
						questionList.add(question);
					}
					screen.setChoices(questionList);
					screen.setImages(imageList);
					Util.closeConnection(null, questionStatement, questionResultset);

					
				}
				Util.closeConnection(connection, screenStatement, screenResultSet);
		} catch (Exception e) {
			e.printStackTrace();
		}

		return screen;
	}
	
	public static ScreenTO getScreenWithId(String id,String lessonId) {

		ScreenTO screen = new ScreenTO();
		try {
				Connection connection = Util.getConnection();
				Statement screenStatement = connection.createStatement();
				String screenSQL = "select * from screens where id=" +id+ " order by lesson_id,s_order";
				// System.out.println("screenSQL:"+screenSQL);
				ResultSet screenResultSet = screenStatement.executeQuery(screenSQL);
			
				while (screenResultSet.next()) {
					screen.setId(screenResultSet.getInt("id"));
					screen.setText(screenResultSet.getString("text"));
					screen.setType(screenResultSet.getString("type"));
					screen.setVideo_url(screenResultSet.getString("video_url"));
					screen.setAudio_url(screenResultSet.getString("audio_url"));
					screen.setQuestion(screenResultSet.getString("question"));

					List<String> imageList = new ArrayList<String>();
					Statement imagesStatement = connection.createStatement();
					String imageSQL = "select * from images where screen_id=" + screenResultSet.getInt("id")
							+ " order by id";
					// System.out.println("imageSQL:"+imageSQL);
					ResultSet imagesResultset = imagesStatement.executeQuery(imageSQL);
					while (imagesResultset.next()) {
						imageList.add(Settings.BASE_URL+lessonId+"/screens/"+id+"/images/"+imagesResultset.getInt("id"));
					}
					Util.closeConnection(null, imagesStatement, imagesResultset);

					List<String> questionList = new ArrayList<String>();
					Statement questionStatement = connection.createStatement();
					String questionSQL = "select * from questions where screen_id=" + screenResultSet.getInt("id")
							+ " order by q_order ";
					// System.out.println("questionSQL:"+questionSQL);
					ResultSet questionResultset = questionStatement.executeQuery(questionSQL);
					while (questionResultset.next()) {
						questionList.add(Settings.BASE_URL+lessonId+"/screens/"+id+"/options/"+questionResultset.getInt("id"));
					}
					screen.setQuestions(questionList);
					screen.setImages(imageList);
					Util.closeConnection(null, questionStatement, questionResultset);

					
				}
				Util.closeConnection(connection, screenStatement, screenResultSet);
		} catch (Exception e) {
			e.printStackTrace();
		}

		return screen;
	}

	public static List<Lesson> getLessons() {

		List<Lesson> lessonList = new ArrayList<Lesson>();
		try {
			Connection connection = Util.getConnection();
			Statement statement = connection.createStatement();
			ResultSet resultSet = statement.executeQuery("select * from lessons order by id");
			while (resultSet.next()) {
				Lesson lesson = new Lesson();
				lesson.setId(resultSet.getInt("id"));
				lesson.setTitle(resultSet.getString("title"));
				lesson.setDescription(resultSet.getString("description"));
				lesson.setScreenCount(resultSet.getInt("screencount"));
				lesson.setUrl(Settings.BASE_URL+resultSet.getInt("id"));
				lessonList.add(lesson);
			}

		} catch (Exception e) {
			e.printStackTrace();
		}
		return lessonList;

	}
	
	
	public static List<String> getLessonURLList() {

		List<String> lessonList = new ArrayList<String>();
		try {
			Connection connection = Util.getConnection();
			Statement statement = connection.createStatement();
			ResultSet resultSet = statement.executeQuery("select * from lessons order by id");
			while (resultSet.next()) {
				lessonList.add(Settings.BASE_URL+""+resultSet.getInt("id"));
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		return lessonList;

	}

}
