package usis;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

import to.QuestionTO;

public class QuestionDAO {
	
	
	public static QuestionTO getQuestionWithId(String id){
		QuestionTO questionTO=new QuestionTO();
		try {
			Connection connection = Util.getConnection();
			Statement questionStatement = connection.createStatement();
			String imageSQL = "SELECT id,title,detail,q_order FROM questions where  id=" +id;
			ResultSet questionResultSet = questionStatement.executeQuery(imageSQL);
			
			while (questionResultSet.next()) {
				questionTO.setTitle(questionResultSet.getString("title"));
				questionTO.setDetail(questionResultSet.getString("detail"));
				questionTO.setOrder(questionResultSet.getInt("q_order"));
			}
			Util.closeConnection(connection, questionStatement, questionResultSet);
		} catch (Exception e) {
			e.printStackTrace();
		}
		
		return questionTO;
		
	} 
	
	public static List<Questions> getListOfOptionsByScreenId(String screenId){
		List<Questions> questionList = new ArrayList<Questions>();
		try {
			Connection connection = Util.getConnection();
			
			Statement questionStatement = connection.createStatement();
			String questionSQL = "select * from questions where screen_id=" + screenId
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
		} catch (Exception e) {
			e.printStackTrace();
		}
		
		return questionList;
		
	} 
}
