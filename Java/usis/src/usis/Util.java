package usis;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.Statement;

public class Util {

	public static Connection getConnection() {
		try {
			
			String url = "jdbc:mysql://localhost:3306/usis";
			Class.forName ("com.mysql.jdbc.Driver").newInstance ();
			Connection connection = DriverManager.getConnection (url, "root", "pass");
			return connection;
		} catch (Exception e) {
			e.printStackTrace();
		}
		return null;

	}

	public static void closeConnection(Connection connection, Statement statement, ResultSet resultSet) {
		try {
			if (resultSet != null && !resultSet.isClosed()) {
				resultSet.close();
			}
		} catch (Exception e) {
			e.printStackTrace();
		}

		try {
			if (statement != null && !statement.isClosed()) {
				statement.close();
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		try {
			if (connection != null && !connection.isClosed()) {
				connection.close();
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public static void closeConnection(Connection connection, Statement statement) {

		try {
			if (statement != null && !statement.isClosed()) {
				statement.close();
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		try {
			if (connection != null && !connection.isClosed()) {
				connection.close();
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public static void closeConnection(Connection connection) {

		try {
			if (connection != null && !connection.isClosed()) {
				connection.close();
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
}
