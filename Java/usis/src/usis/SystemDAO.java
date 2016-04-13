package usis;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

import settings.Settings;
import to.UserTO;

public class SystemDAO {

	
	public static List<String> getUsersURLList() {

		List<String> userList = new ArrayList<String>();
		try {
			Connection connection = Util.getConnection();
			Statement statement = connection.createStatement();
			ResultSet resultSet = statement.executeQuery("select * from system_users order by id");
			while (resultSet.next()) {
				userList.add(Settings.BASE_URL_SYSTEM+""+resultSet.getInt("id")+"/user");
			}
			Util.closeConnection(connection, statement, resultSet);
		} catch (Exception e) {
			e.printStackTrace();
		}
		return userList;

	}
	
	public static UserTO getUser(String id) {

		UserTO  userTO= new UserTO();
		try {
			Connection connection = Util.getConnection();
			Statement statement = connection.createStatement();
			ResultSet resultSet = statement.executeQuery("select * from system_users where id="+id);
			while (resultSet.next()) {
				userTO.setPassword(resultSet.getString("password"));
				userTO.setUsername(resultSet.getString("username"));
			}
			Util.closeConnection(connection, statement, resultSet);
		} catch (Exception e) {
			e.printStackTrace();
		}
		return userTO;

	}
	
	public static boolean login(UserTO userTO) {

		boolean logged=false;
		try {
			Connection connection = Util.getConnection();
			Statement statement = connection.createStatement();
			ResultSet resultSet = statement.executeQuery("select * from system_users where username='"+userTO.getUsername()+"' and password='"+userTO.getPassword()+"'");
			while (resultSet.next()) {
				logged=true;
			}
			Util.closeConnection(connection, statement, resultSet);
		} catch (Exception e) {
			e.printStackTrace();
		}
		return logged;

	}

	
}
