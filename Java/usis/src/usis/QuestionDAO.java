package usis;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;

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
}
