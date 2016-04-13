package usis;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;

import to.ImageTO;

public class ImageDAO {
	
	public static ImageTO getImageWithId(String id) {
		ImageTO image = new ImageTO();
		try {
			Connection connection = Util.getConnection();
			Statement imageStatement = connection.createStatement();
			String imageSQL = "select id,url,title,screen_id from images where id=" +id;
			ResultSet imageResultSet = imageStatement.executeQuery(imageSQL);
			
			while (imageResultSet.next()) {
				image.setTitle(imageResultSet.getString("title"));
				image.setUrl(imageResultSet.getString("url"));
			}
			Util.closeConnection(connection, imageStatement, imageResultSet);
		} catch (Exception ee) {
			ee.printStackTrace();
		}
		return image;
	}
	
}
